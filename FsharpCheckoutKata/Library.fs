namespace FsharpCheckoutKata

module Checkout =
    type Discount = 
        { Sku : string
          AmountRequired : int
          DiscountedAmount : decimal }

    let discounts =
        [  { Sku = "A99"; AmountRequired = 3; DiscountedAmount = -0.20m };
           { Sku = "B15"; AmountRequired = 2; DiscountedAmount = -0.15m }; ]   
        
    let products =
        [  "A99", 0.50m;
           "B15", 0.30m;
           "C40", 0.60m; ]
        |> Map.ofList

    let calculateSubTotal items =
        items
        |> List.choose (fun (sku, quantity) ->
            match products.TryFind sku with
            | Some price -> Some (price * decimal quantity)
            | None -> None)
        |> List.sum

    let applyDiscounts items =
        items
        |> List.choose (fun (sku, quantity) ->
            let discount = discounts |> List.tryFind (fun d -> d.Sku = sku && d.AmountRequired <= quantity)
            match discount with
            | Some discount -> Some (discount.DiscountedAmount * decimal (quantity / discount.AmountRequired))
            | None -> None)
        |> List.sum

    let calculateTotalPrice items =
        let subtotal = calculateSubTotal items
        let discountTotal = applyDiscounts items
        subtotal + discountTotal
        
    let totalPrice items =
        items
        |> List.countBy (fun sku -> sku)
        |> calculateTotalPrice
