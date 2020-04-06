using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WisePriceClient.Models
{
  public class Deal
  {
    public int DealId { get; set; }

    public static List<Deal> GetDeals()
    {
      var apiCallTask = ApiHelper.GetAll();
      var result = apiCallTask.Result;

      JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(result);
      List<Deal> dealList = JsonConvert.DeserializeObject<List<Deal>>(jsonResponse.ToString());

      return dealList;
    }

    public static Deal GetDetails(int id)
    {
      var apiCallTask = ApiHelper.GetSingleDeal(id);
      var result = apiCallTask.Result;
      JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
      Deal deal = JsonConvert.DeserializeObject<Deal>(jsonResponse.ToString());
      return deal;
    }

    public static void AddDeal(Deal newDeal)
    {
      string jsonReview = JsonConvert.SerializeObject(newDeal);
      var apiCallTask = ApiHelper.Post(jsonReview);
    }

    public static void Update(Deal dealToEdit)
    {
      string jsonReview = JsonConvert.SerializeObject(dealToEdit);
      var apiCallTask = ApiHelper.Put(dealToEdit.DealId, jsonReview);
    }

    public static void Delete(int id)
    {
      var apiCallTask = ApiHelper.Delete(id);
    }
  }
}