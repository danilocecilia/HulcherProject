CREATE VIEW dbo.CS_View_Customer_Contact_Phone  
AS  

SELECT DISTINCT   
	 cust.CustomerNumber					AS [CustomerNumber]
    ,cust.Name								AS [CustomerName]
	,cust.AlertNotification					AS [CustomerNotes]
    ,cust.HomePhoneCodeArea					AS [CustomerHomePhoneCodeArea]
	,cust.Phone								AS [CustomerHomePhone]
	,cust.BillingHomePhoneCodeArea			AS [CustomerBillPhoneCodeArea]
    ,cust.BillPhone							AS [CustomerBillPhone]
    ,cust.FaxCodeArea						AS [CustomerFaxCodeArea]
    ,cust.Fax								AS [CustomerFax]
    ,cust.BillFaxCodeArea					AS [CustomerBillFaxCodeArea]
    ,cust.BillFax							AS [CustomerBillFax]
    ,contact.ID								AS [ContactID]
    ,contact.DynamicsContact				AS [ContactDynamics]
    ,contact.Attn							AS [ContactAttn]
	,contact.Name							AS [ContactName]
    ,contact.LastName						AS [ContactLastName]
    ,contact.Alias							AS [ContactAlias]
    ,contact.HomePhoneCodeArea				AS [ContactHomePhoneCodeArea]
    ,contact.Phone							AS [ContactHomePhone]
    ,contact.FaxCodeArea					AS [ContactFaxCodeArea]
    ,contact.Fax							AS [ContactFax]
    ,contact.BillingHomePhoneCodeArea		AS [ContactBillPhoneCodeArea]
    ,contact.BillPhone						AS [ContactBillPhone]
    ,contact.BillFaxCodeArea				AS [ContactBillFaxCodeArea]
    ,contact.BillFax						AS [ContactBillFax]
    ,phone.Number							AS [ContactAdditionalNumber]
    ,phoneType.Name							AS [CustomerAdditionalPhoneType]
FROM
	dbo.CS_Customer AS cust 
		LEFT OUTER JOIN dbo.CS_Customer_Contact AS cust_contact ON cust.ID = cust_contact.CustomerID 
			INNER JOIN dbo.CS_Contact AS contact ON contact.ID = cust_contact.ContactID 
				LEFT OUTER JOIN dbo.CS_PhoneNumber AS phone ON contact.ID = phone.ContactID
					LEFT OUTER JOIN  dbo.CS_PhoneType AS phoneType ON phoneType.ID = phone.PhoneTypeID  
WHERE
	((cust.Phone IS NOT NULL) AND (cust.Phone <> '')) OR  
	((contact.Phone IS NOT NULL) AND (contact.Phone <> '')) OR  
    ((phone.Number IS NOT NULL) AND (phone.Number <> '')) OR  
    ((cust.BillPhone IS NOT NULL) AND (cust.BillPhone <> '')) OR  
    ((cust.Fax IS NOT NULL) AND (cust.Fax <> '')) OR  
    ((cust.BillFax IS NOT NULL) AND (cust.BillFax <> '')) OR  
    ((contact.Fax IS NOT NULL) AND (contact.Fax <> '')) OR  
    ((contact.BillPhone IS NOT NULL) AND (contact.BillPhone <> '')) OR  
    ((contact.BillFax IS NOT NULL) AND (contact.BillFax <> ''))  