namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class InvitationInfo
    {
        public string AccessCode { get; set; }

        public DateTime? AccessCodeExpired { get; set; }

        public bool CanShare { get; set; }

        public bool CanSurvey { get; set; }

        public DateTime? CompleteDate { get; set; }

        public DateTime CreateDate { get; set; }

        public string Data { get; set; }

        public int Id { get; set; }

        public bool IsExpired { get; set; }

        public bool IsShared { get; set; }

        public bool IsSurveyed { get; set; }

        public string Language { get; set; }

        public int ObjectId { get; set; }

        public string ObjectName { get; set; }

        public string ObjectType { get; set; }

        public string ShareCode { get; set; }

        public bool Verified { get; set; }
    }
}

