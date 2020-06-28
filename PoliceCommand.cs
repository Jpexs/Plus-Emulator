using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Plus.Utilities;
using Plus.HabboHotel.Users;
using Plus.HabboHotel.GameClients;


using Plus.HabboHotel.Moderation;

using Plus.Database.Interfaces;
using Plus.Communication.Packets.Outgoing.Rooms.Engine;

namespace Plus.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class PoliceCommand : IChatCommand
    {

        public string PermissionRequired
        {
            get { return ""; }
        }

        public string Parameters
        {
            get { return ""; }
        }

        public string Description
        {
            get { return "Faz de você um oficial da lei!"; ; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {

            RoomUser ThisUser = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (Session.GetHabbo().Rank >= 2 && Session.GetHabbo().isOfficer == false)
            {
                Session.GetHabbo().isOfficer = true;
                ThisUser.ApplyEffect(19);
                Session.SendWhisper("Você é agora um policial e pode cuida do jogo agora!");

                Session.GetHabbo()._NamePrefixColor = "d15000";
                Session.GetHabbo()._NamePrefix = "Police";

                RoomUser User = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
                if (User != null)
                {
                    Session.SendMessage(new UserChangeComposer(User, true));
                    Room.SendMessage(new UserChangeComposer(User, false));
                }

            }

            else if (Session.GetHabbo().Rank >= 3 && Session.GetHabbo().isOfficer == true)
            {
                Session.GetHabbo().isOfficer = false;
                ThisUser.ApplyEffect(0);
                Session.SendWhisper("Você não é um oficial e não pode prender pessoas mais!");
            }
            else
            {
                Session.SendWhisper("Você não tem permissão para usar isso!");
            }
        }
    }
}
