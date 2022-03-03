using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country
{
    public enum Format
    {
        ODI,
        TEST,
        T20
    }

    public string CountryName;
    public Format MatchFormat;
}
