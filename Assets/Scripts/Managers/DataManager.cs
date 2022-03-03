using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class DataManager : IManager
{
    public void Init()
    {

    }

    private T LoadData<T>(string path)
    {
        TextAsset textAsset = Resources.Load(path) as TextAsset;
        string data;
        if (textAsset == null)
        {
            data = "";
        }
        else
        {
            data = textAsset.text;
        }
        T objData = (T)JsonConvert.DeserializeObject(data);

        return objData;
    }

    public List<Country> LoadCountriesData()
    {
        List<Country> countries = new List<Country>();
        JArray charactersData = LoadData<JArray>("Data/Country");

        for (int i = 0; i < charactersData.Count; i++)
        {
            Country country = new Country
            {
                CountryName = charactersData[i]["CountryName"].Value<string>()
            };
            countries.Add(country);
        }

        return countries;
    }

    public List<CardData> LoadPlayersData(string countryName, string format)
    {
        List<CardData> playerList = new List<CardData>();
        JArray playerDatas = LoadData<JArray>("Data/PlayerData/" + countryName+"/"+format+"/Players");

        for (int i = 0; i < playerDatas.Count; i++)
        {
            playerList.Add(
                new CardData
                {
                    PlayerName = playerDatas[i]["PlayerName"].Value<string>(),
                    MatchesPlayed = playerDatas[i]["MatchesPlayed"].Value<int>(),
                    RunsScored = playerDatas[i]["RunsScored"].Value<int>(),
                    Centuries = playerDatas[i]["Centuries"].Value<int>(),
                    Fifties = playerDatas[i]["Fifties"].Value<int>(),
                    HighestScore = playerDatas[i]["HighestScore"].Value<int>(),
                    BattingAverage = playerDatas[i]["BattingAverage"].Value<float>(),
                    Wickets = playerDatas[i]["Wickets"].Value<int>(),
                    Catches = playerDatas[i]["Catches"].Value<int>()
                }
            );
        }

        return playerList;
    }    
}