module Tests

open Xunit
open FsharpCheckoutKata

[<Theory>]
[<InlineData("A99", 0.50)>]
[<InlineData("B15", 0.30)>]
[<InlineData("C40", 0.60)>]
let ``Get Correct Price`` sku price =
    let total = Checkout.totalPrice [ sku ]
    Assert.Equal(total, price)

let multipleItems : obj [] [] = 
    [|
       [| [ "A99"; "A99" ]; 1.00m |]
       [| [ "C40"; "C40" ]; 1.20m |]
       [| [ "A99"; "C40" ]; 1.10m |]
    |]

[<Theory>]
[<MemberData("multipleItems")>]
let ``Get Correct Price For Multiple Items`` items price =
    let total = Checkout.totalPrice items
    Assert.Equal(total, price)

let multipleOfferItems : obj [] [] = 
    [|
       [| [ "A99"; "A99"; "A99" ]; 1.30m |]
       [| [ "B15"; "B15" ]; 0.45m |]
       [| [ "A99"; "A99"; "A99"; "B15"; "B15";"A99"; "A99"; "B15" ]; 3.05m |]
    |]

[<Theory>]
[<MemberData("multipleOfferItems")>]
let ``Get Correct Price For Multiple Offer Items`` items price =
    let total = Checkout.totalPrice items
    Assert.Equal(total, price)


