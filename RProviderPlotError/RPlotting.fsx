#I @"..\packages\"
#r @"R.NET.Community.1.6.5\lib\net40\RDotNet.dll"
#r @"R.NET.Community.FSharp.1.6.5\lib\net40\RDotNet.FSharp.dll"
#r @"RProvider.1.1.20\lib\net40\RPRovider.dll"
#r @"RProvider.1.1.20\lib\net40\RPRovider.Runtime.dll"
#r @"FSharp.Data.2.3.2\lib\net40\FSharp.Data.dll"

open System
open FSharp.Data
open RProvider.``base``
open RProvider.graphics
open RProvider.Helpers

let wb = WorldBankData.GetDataContext()

let countries = [|
    wb.Countries.Canada;
    wb.Countries.``United States``;
    wb.Countries.Mexico;
    wb.Countries.Brazil;
    wb.Countries.Argentina;
    wb.Countries.``United Kingdom``;
    wb.Countries.France;
    wb.Countries.Germany;
    wb.Countries.``South Africa``;
    wb.Countries.Kenya;
    wb.Countries.``Russian Federation``;
    wb.Countries.China;
    wb.Countries.Japan;
    wb.Countries.Australia |]

let gdp2000 = countries |> Array.map (fun c -> c.Indicators.``GDP (current US$)``.[2000])
let gdp2010 = countries |> Array.map (fun c -> c.Indicators.``GDP (current US$)``.[2010])

let series = [
    "GDP2000", gdp2000;
    "GDP2010", gdp2010; ]

let dataframe = R.data_frame(namedParams series)
R.plot(dataframe)

let names = countries |> Array.map (fun c -> c.Name)
R.text(gdp2000, gdp2010, names)