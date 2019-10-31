using System;
using static NovaBot.Helpers.EnumerablesHelper;

namespace NovaBot.Models.ViewModels
{

public class ListQuoteRequestModel{
    public OrderByEnum OrderBy {get; set;}
    public int Page {get; set;}
    public int N {get; set;}
}

}
