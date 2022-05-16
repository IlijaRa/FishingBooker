using FishingBookerLibrary.DataAccess;
using FishingBookerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingBookerLibrary.BusinessLogic
{
    public class ClientComplaintCRUD
    {
        public static int CreateClientComplaint(string ownerId,
                                                string ownerName,
                                                string ownerSurname,
                                                string ownerEmail,
                                                string clientsEmail,
                                                string actionTitle,
                                                string reason)
        {
            ClientComplaint data = new ClientComplaint
            {
                OwnerId = ownerId,
                OwnerName = ownerName,
                OwnerSurname = ownerSurname,
                OwnerEmailAddress = ownerEmail,
                ClientsEmailAddress = clientsEmail,
                ActionTitle = actionTitle,
                Reason = reason
            };

            string sql = @"INSERT INTO dbo.ClientComplaints (OwnerId, OwnerName, OwnerSurname, OwnerEmailAddress, ClientsEmailAddress, ActionTitle, Reason)
                           VALUES (@OwnerId, @OwnerName, @OwnerSurname, @OwnerEmailAddress, @ClientsEmailAddress, @ActionTitle, @Reason);";

            return SSMSDataAccess.SaveData(sql, data);
        }
    }
}