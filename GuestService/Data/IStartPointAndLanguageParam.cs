namespace GuestService.Data
{
    using System;

    public interface IStartPointAndLanguageParam
    {
        string Language { get; }

        string ln { get; set; }

        int? sp { get; set; }

        string spa { get; set; }

        int? StartPoint { get; }

        string StartPointAlias { get; }
    }
}

