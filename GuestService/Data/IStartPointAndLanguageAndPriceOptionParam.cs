namespace GuestService.Data
{
    using System;

    public interface IStartPointAndLanguageAndPriceOptionParam : IStartPointAndLanguageParam
    {
        bool WithoutPrice { get; }

        bool? wp { get; set; }
    }
}

