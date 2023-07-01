This is an API based solution to generate an Invoice. 

Input is buyer and seller information as well as service information.
Output is a pdf of an invoice.

Example input json for testing purposes:

{
  "buyerCompanyName": "PerkuSau",
  "buyerAddress": "Gargzdu g. 1, Vilnius",
  "buyerVatCode": "LT300009118213",
  "buyerCompanyCode": "455585437",
  "buyerCountry": "LT",
  "sellerCompanyName": "Babilonas",
  "sellerAddress": "Verkiu g. 1, Vilnius",
  "sellerVatCode": "LT100009118213",
  "sellerCompanyCode": "303485437",
  "sellerCountry": "LT",
  "serviceName": "Vanilinis Cukrus",
  "sellerBank": "Swedbank, AB",
  "sellerBankAccountNumber": "LT4100000000000000",
  "units": 1,
  "unitPrice": 10
}

Input field restrictions:
All fields are mandatory
String values have to be non empty.
Countries have to be in the supported country list ("LT", "JP", "CA", "FR").
Units and Unit price have to be more than 0.
