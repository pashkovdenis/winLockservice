using Cassia;
using EncryptionService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using WinGuard.Domain.Interface;
using WinGuard.Domain.Model;
using WinGuard.Windows.Client.App.Interface;
using WinGuard.Windows.Client.App.Services;

namespace WinGuardService
{
    public partial class WinGuardService : ServiceBase
    {
        private System.Timers.Timer _guardTimer;
        private readonly IClientAuthService _authService;
        private readonly IMessageReader _msgReader;
        private readonly IEncryptionService _encryptService; 


        public WinGuardService()
        {
            InitializeComponent();
            CanPauseAndContinue = true;
            CanHandleSessionChangeEvent = true;
            _guardTimer = new System.Timers.Timer();
            _guardTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _guardTimer.Interval = 3000;
            _guardTimer.Start();
            _authService = new WinGuardClientAuthService(ConfigurationManager.AppSettings["Server"]);
            _msgReader = new MessageReaderService(ConfigurationManager.AppSettings["Server"]);
            _encryptService = new EncryptDecryptService(Keys.AesIV256, Keys.AesKey256);

        }
        protected override void OnStart(string[] args) => ValidateSessionsAsync();
        private void OnTimedEvent(object source, ElapsedEventArgs e) => ValidateSessionsAsync(); 
        private void ValidateSessionsAsync()
        {
            ThreadPool.QueueUserWorkItem(async (object o) =>
            {
                ITerminalServicesManager manager = new TerminalServicesManager();
                using (ITerminalServer server = manager.GetLocalServer())
                {
                    server.Open();

                    var localSessions = server.GetSessions().Where(s => s.SessionId > 0 && (s.ConnectionState == Cassia.ConnectionState.Active));
                    var messages = await _msgReader.GetAllMessages();

                    if (messages.Count(m => m.Command.Type == WinGuard.Domain.Enumaretions.ClientCommandType.LOGOUT) > 0 &&
                             localSessions.Any(s => messages.Any(m => m.Command.ClientIdentifier == s.SessionId.ToString())))
                    {
                        foreach (var session in localSessions.Where(s => messages.Any(m => m.Command.ClientIdentifier == s.SessionId.ToString())))
                        {
                            session.Logoff(); 
                        }
                        return; 
                    }
                    foreach (ITerminalServicesSession session in localSessions)  
                        await _authService.Login(_encryptService.EncryptMessage(session.SessionId.ToString()),
                             _encryptService.EncryptMessage(session.UserName));

                }

            });
        }
    }
}
