using System;

namespace DTO.Contacts
{
    public class SearchContactDTO
    {
        public SearchContactDTO()
        {
            onlyActive = false;
            onlyInactive = false;
            onlyWithPicture = false;
            onlyWithoutPicture = false;
            createdSince = null;
            createdUntil = null;
        }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string genre { get; set; }
        public string email { get; set; }
        public string mobileNumber { get; set; }
        public string landlineNumber { get; set; }
        public bool onlyActive { get; set; }
        public bool onlyInactive { get; set; }
        public bool onlyWithPicture { get; set; }
        public bool onlyWithoutPicture { get; set; }
        public string comments { get; set; }
        public DateTime? createdSince { get; set; }
        public DateTime? createdUntil { get; set; }
    }
}
