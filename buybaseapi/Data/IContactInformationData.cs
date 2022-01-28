using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using buybaseapi.Models;

namespace buybaseapi.Data
{
    public interface IContactInformationData
    {
        ContactInformation GetContactInformation(Guid id);
        ContactInformation AddContactInformation(ContactInformation contactInformation);
        void DeleteContactInformation(ContactInformation contactInformation);
        int GetCountOfContactsOfLocation(string location);

    }
}
