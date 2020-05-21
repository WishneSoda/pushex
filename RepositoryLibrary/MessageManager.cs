using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    public class MessageManager
    {
        private IMessage _Messager;
        public MessageManager(IMessage Messager)
        {
            _Messager = Messager;
        }

        public void SendMessage(string msg)
        {
            _Messager.SendMessage(msg);
        }
    }
}

