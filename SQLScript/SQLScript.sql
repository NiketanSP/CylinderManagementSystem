
USE [New_Cylinder_Final_Updated_2021]
GO
/****** Object:  StoredProcedure [dbo].[get_Cylinder_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[get_Cylinder_Details]
(
@item_no nvarchar(50)
)
AS begin
SELECT CONVERT(DATE,Fill_Date,103) AS DATE, CONCAT('SEND TO ', Supplier_Name) AS STATUS, Fill_ID AS DC_NO
FROM Tb_Fill_Master
WHERE Fill_ID IN (SELECT Fill_ID FROM Tb_Fill_Content_Master WHERE Item_No = @item_no)
UNION ALL
SELECT CONVERT(DATETIME,A.Received_Date,103) AS DATE,CONCAT(A.Status,' - ', B.Supplier_Name) AS STATUS, Supp_Dc_No AS DC_NO
FROM Tb_Fill_Content_Master A JOIN Tb_Fill_Master B ON A.Fill_ID = B.Fill_ID 
WHERE A.Item_No = @item_no AND A.Received_Date != 'NA'
UNION ALL
SELECT CONVERT(DATE,C_Dc_Date,103) AS DATE, CONCAT('SEND TO ', C_Name) AS STATUS, C_Dc_No AS DC_NO
FROM Tb_CDC_Details
WHERE C_Dc_No IN (SELECT C_Dc_No FROM Tb_CDC_Content_Details WHERE Item_No = @item_no)
UNION ALL 
SELECT CONVERT(DATETIME,A.Received_Date,103) AS DATE,CONCAT('RECEIVED FROM',' - ', B.C_Name) AS STATUS, Cust_Dc_No AS DC_NO
FROM Tb_CDC_Content_Details A JOIN Tb_CDC_Details B ON A.C_Dc_No = B.C_Dc_No 
WHERE A.Item_No = @item_no AND A.Received_Date != 'NA'
ORDER BY DATE DESC

End






GO
/****** Object:  StoredProcedure [dbo].[Insert_Asset_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Insert_Asset_Details]
(
@Purchase_Date nvarchar(100)
,@Asset_Name nvarchar(50)
,@Purchase_Cost nvarchar(100)
,@Asset_Type nvarchar(50)
)
As Begin

insert Tb_Asset_Details values
(
@Purchase_Date
,@Asset_Name
,@Purchase_Cost
,@Asset_Type
)
End





















GO
/****** Object:  StoredProcedure [dbo].[Insert_Cust_DC_Content]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[Insert_Cust_DC_Content]
(
@dc_no nvarchar(50)
,@cy_type nvarchar(50)
,@particular nvarchar(50)
,@sr_no nvarchar(50)
,@item_no nvarchar(50)
,@quantity nvarchar(50)
,@rate nvarchar(50) 
,@unit nvarchar(50)
,@total nvarchar(50)
,@sell_status nvarchar(50)
,@received_status nvarchar(50)
)
As Begin

insert Tb_CDC_Content_Details
([C_Dc_No]
           ,[Cy_Type]
           ,[Particulars]
           ,[Sr_No]
           ,[Item_No]
           ,[Quantity]
           ,[Rate]
           ,[Unit]
           ,[Total]
           ,[Sell_Status]
           ,[Receive_Status])
 values
(
@dc_no
,@cy_type
,@particular
,@sr_no
,@item_no
,@quantity
,@rate
,@unit
,@total
,@sell_status
,@received_status
)
End

























GO
/****** Object:  StoredProcedure [dbo].[Insert_Cust_DC_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[Insert_Cust_DC_Details]
(
@dc_date nvarchar(50)
,@dc_no nvarchar(50)
,@cust_name nvarchar(50)
,@total_items nvarchar(50)
,@total_amt nvarchar(50)
,@paid_amt nvarchar(50)
,@balance_amt nvarchar(50)
,@remark nvarchar(200)
,@pay_mode nvarchar(50)
,@reference nvarchar(50) 
,@deposit_Amt nvarchar(50)
,@deposit_Return nvarchar(50)
,@for_Days nvarchar(50)
,@reason nvarchar(50)
,@cancel_status nvarchar(50)
,@tax_Status nvarchar(50)
,@C_ID bigint
,@Vehicle_No varchar(50)
)
As Begin

insert Tb_CDC_Details
([C_Dc_Date]
           ,[C_Dc_No]
           ,[C_Name]
           ,[Total_Items]
           ,[Total_Amt]
           ,[Paid_Amt]
           ,[Balance_Amt]
           ,[Remark]
           ,[Pay_Mode]
           ,[Pay_Reference]
           ,[Deposit_Amt]
           ,[Deposit_Return]
           ,[For_Days]
           ,[Reason]
		   ,[Status]
		   ,[Tax_Status]
		   ,[C_ID]
		   ,[Vehicle_No])
 values
(
@dc_date
,@dc_no
,@cust_name
,@total_items
,@total_amt
,@paid_amt
,@balance_amt
,@remark
,@pay_mode
,@reference 
,@deposit_Amt
,@deposit_Return
,@for_Days
,@reason
,@cancel_status
,@tax_Status
,@C_ID
,@Vehicle_No
)

End





















GO
/****** Object:  StoredProcedure [dbo].[Insert_Customer_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[Insert_Customer_Master]
(
@C_InDate nvarchar(100)
,@Cust_Name nvarchar(100)
,@Cust_Address nvarchar(100)
,@Cust_PhoneNo nvarchar(50)
,@Cust_GSTNo nvarchar(50)
,@Cust_CPerson nvarchar(100)
,@CP_PhoneNo nvarchar(50)
,@Cust_Status nvarchar(50)
)
As Begin

insert Tb_Customer_Master values
(
@C_InDate
,@Cust_Name
,@Cust_Address
,@Cust_PhoneNo
,@Cust_GSTNo
,@Cust_CPerson
,@CP_PhoneNo
,@Cust_Status
)
End

























GO
/****** Object:  StoredProcedure [dbo].[Insert_Cylinder_Agency_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Insert_Cylinder_Agency_Master]
(
@Com_ID bigint
,@Com_Name nvarchar(50)
,@Com_Depo_Address nvarchar(100)
,@Com_Office_Address nvarchar(100)
,@Com_Phone nvarchar(50)
,@Com_Mobile nvarchar(50)
,@Com_GST_No nvarchar(50)
,@Com_Pan_No nvarchar(50)
,@Com_AccNo nvarchar(50)
,@Com_IFSC nvarchar(50)
,@Com_Logo image
,@Com_Bank nvarchar(50)
,@Com_Email nvarchar(50)
,@Com_Website nvarchar(50)
)
As Begin

insert Tb_Cylinder_Agency_Master values
(
@Com_ID
,@Com_Name
,@Com_Depo_Address
,@Com_Office_Address
,@Com_Phone
,@Com_Mobile
,@Com_GST_No
,@Com_Pan_No
,@Com_AccNo
,@Com_IFSC
,@Com_Logo
,@Com_Bank
,@Com_Email
,@Com_Website
)
End























GO
/****** Object:  StoredProcedure [dbo].[Insert_Cylinder_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Insert_Cylinder_Master]
(
@C_ID bigint
,@C_P_No bigint
,@C_Name nvarchar(100)
,@Com_Phone bigint
,@C_Rate bigint
,@C_Type nvarchar(50)
)
As Begin

insert Tb_Cylinder_Master values
(
@C_ID
,@C_P_No
,@C_Name
,@C_Rate
,@C_Type
)
End






















GO
/****** Object:  StoredProcedure [dbo].[Insert_Expenses_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[Insert_Expenses_Details]
(
@Ex_Date	nvarchar(50)	
,@Ex_Type	nvarchar(50)	
,@Vehicle_No	nvarchar(50)	
,@Person_Name	nvarchar(50)	
,@Ex_Amount	nvarchar(50)	
,@Ex_Description	nvarchar(50)	
,@Wages_Type	nvarchar(50)
)
As Begin

insert Tb_Expenses_Master values
(
@Ex_Date
,@Ex_Type
,@Vehicle_No
,@Person_Name
,@Ex_Amount
,@Ex_Description
,@Wages_Type
)
End



















GO
/****** Object:  StoredProcedure [dbo].[Insert_Fill_Content]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Insert_Fill_Content]
(
@Item_No nvarchar(100)
,@Sr_No nvarchar(100)
,@Name nvarchar(50)
,@Fill_ID nvarchar(50)
,@Rate nvarchar(50)
,@Status nvarchar(50)
,@supp_Dc_No nvarchar(50)
,@received_Date nvarchar(50)
,@Cy_Type nvarchar(50)
)
As Begin

insert Tb_Fill_Content_Master
([Item_No]
           ,[Sr_No]
           ,[Name]
           ,[Fill_ID]
           ,[Rate]
           ,[Status]
           ,[Supp_Dc_No]
           ,[Received_Date]
           ,[Cy_Type])
 values
(
@Item_No
,@Sr_No
,@Name
,@Fill_ID
,@Rate
,@Status
,@supp_Dc_No
,@received_Date
,@Cy_Type
)
End





















GO
/****** Object:  StoredProcedure [dbo].[Insert_Fill_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Insert_Fill_Details]
(
@id bigint
,@Fill_ID nvarchar(50)
,@Fill_Date nvarchar(100)
,@Supplier_Name nvarchar(100)
,@Cylinder_Count bigint
,@Net_Amt nvarchar(50)
,@cancel_status nvarchar(50)
,@paid_amt nvarchar(50)
,@balance_amt nvarchar(50)
,@paid_by nvarchar(50)
,@reference nvarchar(50)
,@supp_invoice_no nvarchar(50)
,@Vehicle_No varchar(50)
)
As Begin

insert Tb_Fill_Master 
([ID]
           ,[Fill_Date]
           ,[Supplier_Name]
           ,[Cylinder_Count]
           ,[Fill_ID]
           ,[Net_Amt]
		   ,[Cancel_status]
		   ,[Paid_Amt]
		   ,[Balance_Amt]
		   ,[Paid_By]
		   ,[Reference]
		   ,[Supp_Invoice_No]
		   ,[Vehicle_No])
values
(
@id
,@Fill_Date
,@Supplier_Name
,@Cylinder_Count
,@Fill_ID
,@Net_Amt
,@cancel_status
,@paid_amt
,@balance_amt
,@paid_by
,@reference
,@supp_invoice_no
,@Vehicle_No
)
End





















GO
/****** Object:  StoredProcedure [dbo].[Insert_Filling_Payment]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[Insert_Filling_Payment]
(
@Fill_DC_No nvarchar(50)
,@Payment_Date nvarchar(50)
,@Invoice_Amt nvarchar(50)
,@Paid_Amt nvarchar(50)
,@Pay_Mode nvarchar(50)
,@reference nvarchar(50)
)
As Begin

insert Tb_Filling_Payment
(
[Fill_DC_No]
,[Payment_Date]
,[Invoice_Amt]
,[Paid_Amt]
,[Payment_Mode]
,[Reference]
)
values
(
@Fill_DC_No
,@Payment_Date
,@Invoice_Amt
,@Paid_Amt
,@Pay_Mode
,@reference
)
End

















GO
/****** Object:  StoredProcedure [dbo].[Insert_Inventory_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[Insert_Inventory_Master]
(
@particulars nvarchar(100)
,@type nvarchar(50)
,@in_stock nvarchar(50)
,@out_stock nvarchar(100)
,@cylinder_type nvarchar(50)
)
As Begin

insert Tb_Inventory_Master
(
[Particulars],
[Type],
[In_Stock],
[Out_Stock],
[Cylinder_Type]
) 
values
(
@particulars
,@type
,@in_stock
,@out_stock
,@cylinder_type
)
End




























GO
/****** Object:  StoredProcedure [dbo].[Insert_Payment_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Insert_Payment_Details]
(
@Cust_Invoice_No nvarchar(50)
,@Supp_Invoice_No nvarchar(50)
,@Payment_Date nvarchar(50)
,@Invoice_Amt nvarchar(50)
,@Paid_Amt nvarchar(50)
,@Pay_Mode nvarchar(50)
,@reference nvarchar(50)
)
As Begin

insert Tb_Payment_Master
(
[Cust_Invoice_No]
,[Supp_Invoice_No]
,[Payment_Date]
,[Invoice_Amt]
,[Paid_Amt]
,[Payment_Mode]
,[Reference]
)
values
(
@Cust_Invoice_No
,@Supp_Invoice_No
,@Payment_Date
,@Invoice_Amt
,@Paid_Amt
,@Pay_Mode
,@reference
)
End

















GO
/****** Object:  StoredProcedure [dbo].[Insert_Purchase_Content_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[Insert_Purchase_Content_Details]
(
@Particulers nvarchar(100)
,@HSN nvarchar(50)
,@Rate nvarchar(50)
,@Disc nvarchar(50)
,@Quantity nvarchar(50)
,@Unit nvarchar(50)
,@Total nvarchar(100)
,@SGST nvarchar(50)
,@CGST nvarchar(50)
,@IGST nvarchar(50)
,@TaxableAmt nvarchar(50)
,@Purchase_Type nvarchar(100)
,@Sr_No nvarchar(50)
,@Part_No nvarchar(50)
,@cylinder_Type nvarchar(50)
,@invoice_No nvarchar(50)
,@cylinder_Status nvarchar(50)
,@Cust_Supp_Name nvarchar(50)
)
As Begin

insert Tb_Purchase_Content_Master
(
[Particulers]
           ,[HSN]
           ,[Rate]
           ,[Disc]
           ,[Quantity]
           ,[Unit]
           ,[Total]
           ,[SGST]
           ,[CGST]
           ,[IGST]
           ,[TaxableAmt]
           ,[Purchase_Type]
           ,[Sr_No]
           ,[Part_No]
           ,[Cylinder_Type]
           ,[Invoice_No]
           ,[Cylinder_Status]
           ,[Cust_Supp_Name])
 values
(
@Particulers
,@HSN
,@Rate
,@Disc
,@Quantity
,@Unit
,@Total
,@SGST
,@CGST
,@IGST
,@TaxableAmt
,@Purchase_Type
,@Sr_No
,@Part_No
,@cylinder_Type
,@invoice_No
,@cylinder_Status
,@Cust_Supp_Name
)
End



























GO
/****** Object:  StoredProcedure [dbo].[Insert_Purchase_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[Insert_Purchase_Details]
(
@Supplier_Name nvarchar(100)
,@Date_Of_Purchase nvarchar(50)
,@Invoice_No nvarchar(100)
,@Gst_No nvarchar(100)
,@SGST_Per nvarchar(50)
,@SGST_Amt nvarchar(50)
,@CGST_Per nvarchar(50)
,@CGST_Amt nvarchar(50)
,@IGST_Per nvarchar(50)
,@IGST_Amt nvarchar(50)
,@total_items nvarchar(50)
,@Total_Amt nvarchar(50)
,@GST_Amt nvarchar(50)
,@Net_Amt nvarchar(50)
,@Paid_Amt nvarchar(50)
,@Balance_Amt nvarchar(50)
,@Payment_Mode nvarchar(50)
,@Reff_No nvarchar(50)
,@Gst_Perecent nvarchar(50)
,@Cancel_status nvarchar(50)
)
As Begin

insert Tb_Purchase values
(
@Supplier_Name 
,@Date_Of_Purchase
,@Invoice_No
,@GST_No
,@SGST_Per
,@SGST_Amt
,@CGST_Per
,@CGST_Amt
,@IGST_Per
,@IGST_Amt
,@total_items
,@Total_Amt
,@GST_Amt
,@Net_Amt
,@Paid_Amt
,@Balance_Amt
,@Payment_Mode
,@Reff_No
,@Gst_Perecent
,@Cancel_status
)
End




























GO
/****** Object:  StoredProcedure [dbo].[Insert_SignUp_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[Insert_SignUp_Details]
(
@SignUp_Date nvarchar(50)
,@Name nvarchar(50)
,@Designation nvarchar(50)
,@Contact_No nvarchar(50)
,@User_Type nvarchar(50)
,@User_ID nvarchar(50)
,@Password nvarchar(50)
,@Confirm_Password nvarchar(50)

)
As Begin

insert Tb_SignUp_Details values
(
@SignUp_Date 
,@Name 
,@Designation 
,@Contact_No 
,@User_Type 
,@User_ID 
,@Password 
,@Confirm_Password 
)
End












GO
/****** Object:  StoredProcedure [dbo].[Insert_Staff_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Insert_Staff_Details]
(
@St_Name nvarchar(50)
,@St_Address nvarchar(100)
,@St_Phone nvarchar(50)
,@St_Mobile nvarchar(50)
,@St_Aadhar_No nvarchar(50)
,@StEmContact nvarchar(50)
,@St_JoinDate nvarchar(50)
,@St_Designation nvarchar(50)
,@St_Salary nvarchar(50)
,@w_days nvarchar(50)
,@St_WHours nvarchar(50)
,@w_min nvarchar(50)
,@Per_Day_Salary nvarchar(50)
,@Per_Hour_Salary nvarchar(50)
,@St_ResignDate nvarchar(50)
,@St_Reason nvarchar(100)
,@Date_of_Birth nvarchar(100)
,@Account_No nvarchar(50)
,@IFSC nvarchar(50)
,@Bank nvarchar(50)
,@Current_Age nvarchar(50)
,@status nvarchar(50)
,@Staff_ID bigint
)
As Begin

insert Tb_Staff_Details
           ([St_Name]
           ,[St_Address]
           ,[St_Phone]
           ,[St_Mobile]
           ,[St_Aadhar_No]
           ,[St_EmContact]
           ,[St_JoinDate]
           ,[St_Designation]
           ,[St_Salary]
           ,[St_WDays]
           ,[St_WHours]
           ,[St_WMin]
           ,[Per_Day_Salary]
           ,[Per_Hour_Salary]
           ,[St_ResignDate]
           ,[St_Reason]
           ,[Date_of_Birth]
           ,[Account_No]
           ,[IFSC]
           ,[Bank]
           ,[Current_Age]
           ,[Staff_Status]
		   ,[Staff_ID])

values
(
@St_Name
,@St_Address
,@St_Phone
,@St_Mobile
,@St_Aadhar_No
,@StEmContact
,@St_JoinDate
,@St_Designation
,@St_Salary
,@w_days
,@St_WHours
,@w_min
,@Per_Day_Salary
,@Per_Hour_Salary 
,@St_ResignDate 
,@St_Reason
,@Date_of_Birth
,@Account_No
,@IFSC
,@Bank
,@Current_Age
,@status
,@Staff_ID
)
End






















GO
/****** Object:  StoredProcedure [dbo].[Insert_Student_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Insert_Student_Details]
(
@S_ID bigint
,@S_Name nvarchar(100)
,@S_Con_No nvarchar(100)
,@S_Age nvarchar(100)
,@S_Adh_No nvarchar(100)
,@S_Bld_Grp nvarchar(100)

)
As Begin

insert Student values
(
@S_ID
,@S_Name
,@S_Con_No
,@S_Age
,@S_Adh_No
,@S_Bld_Grp
)
End






















GO
/****** Object:  StoredProcedure [dbo].[Insert_Supplier_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Insert_Supplier_Master]
(
@Supp_InDate nvarchar(100)
,@Supp_CompName nvarchar(100)
,@Supp_Address nvarchar(100)
,@Supp_Phone nvarchar(50)
,@Supp_GST nvarchar(50)
,@Supp_ConPerson nvarchar(50)
,@Supp_CPersonNo nvarchar(50)
,@Supp_AccNo nvarchar(50)
,@Supp_IFSC nvarchar(50)
,@status nvarchar(50)
,@Supp_Bank nvarchar(50)
)
As Begin

insert Tb_Supplier_Master values
(
@Supp_InDate
,@Supp_CompName
,@Supp_Address
,@Supp_Phone
,@Supp_GST
,@Supp_ConPerson
,@Supp_CPersonNo
,@Supp_AccNo
,@Supp_IFSC
,@status
,@Supp_Bank
)
End



















GO
/****** Object:  StoredProcedure [dbo].[Insert_Supplier_Material]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Insert_Supplier_Material]
(
@Cylinder_Type nvarchar(50)
,@Particulers nvarchar(50)
,@Rate nvarchar(50)
,@Rate_For nvarchar(50)
,@Revised_Date nvarchar(50)
,@Supp_Name nvarchar(50)
)
As Begin

insert Tb_Supplier_Material_Master values
(
@Cylinder_Type
,@Particulers
,@Rate
,@Rate_For
,@Revised_Date
,@Supp_Name
)
End






















GO
/****** Object:  StoredProcedure [dbo].[Insert_Tax_Content_DC]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[Insert_Tax_Content_DC]
(
@Invoice_No nvarchar(50)
,@Cdc_No nvarchar(50)
,@Total_Item nvarchar(100)
,@Dc_Date nvarchar(50)
,@Dc_Total nvarchar(50)
,@Paid_Amt nvarchar(50)
)
As Begin

insert Tb_Tax_Content_DC 
([Invoice_No]
           ,[C_Dc_No]
           ,[C_Dc_Date]
           ,[Total_Items]
           ,[Total_Amt]
		   ,[Paid_Amt]
           )
values
(
@Invoice_No
,@Cdc_No
,@Dc_Date
,@Total_Item
,@Dc_Total
,@Paid_Amt
)
End



























GO
/****** Object:  StoredProcedure [dbo].[Insert_Tax_Sell_Content_Particular]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[Insert_Tax_Sell_Content_Particular]
(
@Invoice_No nvarchar(50)
,@Gas_Type nvarchar(50)
,@Particulers nvarchar(100)
,@HSN nvarchar(50)
,@Rate nvarchar(50)
,@Disc nvarchar(50)
,@Quantity nvarchar(50)
,@Unit nvarchar(50)
,@Total nvarchar(100)
,@SGST nvarchar(50)
,@CGST nvarchar(50)
,@IGST nvarchar(50)
,@TaxableAmt nvarchar(50)
,@Sr_No nvarchar(50)
,@Part_No nvarchar(50)
,@Status nchar(10)
)
As Begin

insert Tb_Tax_Sell_Content_Particular 
([Invoice_No]
           ,[Gas_Type]
           ,[Particulers]
           ,[HSN]
           ,[Item_No]
           ,[Sr_No]
           ,[Rate]
           ,[Discount]
           ,[Quantity]
           ,[Unit]
           ,[Total]
           ,[SGST]
           ,[CGST]
           ,[IGST]
           ,[Taxable_Amt]
		   ,[Sell_Status])
values
(
@Invoice_No
,@Gas_Type
,@Particulers
,@HSN
,@Part_No
,@Sr_No
,@Rate
,@Disc
,@Quantity
,@Unit
,@Total
,@SGST
,@CGST
,@IGST
,@TaxableAmt
,@Status
)
End



























GO
/****** Object:  StoredProcedure [dbo].[Insert_Tax_Sell_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[Insert_Tax_Sell_Details]
(
@ID bigint
,@Invoice_No nvarchar(100)
,@Cust_Name nvarchar(100)
,@Date_Of_Sell nvarchar(50)
,@Gst_No nvarchar(100)
,@Total_Items nvarchar(50)
,@Total_Quantity nvarchar(50)
,@SGST_Amt nvarchar(50)
,@CGST_Amt nvarchar(50)
,@IGST_Amt nvarchar(50)
,@GST_Amt nvarchar(50)
,@Total_Amt nvarchar(50)
,@Net_Amt nvarchar(50)
,@Status nvarchar(50)
,@Paid_Amt nvarchar(50)
,@Balance_Amt nvarchar(50)
)
As Begin

insert Tb_Tax_Sell_Details
([invoice_id]
           ,[Invoice_No]
           ,[Cust_Name]
           ,[Sell_Date]
           ,[Gst_No]
           ,[Total_Items]
           ,[Total_Quantity]
           ,[Sgst_Amt]
           ,[Cgst_Amt]
           ,[Igst_Amt]
           ,[Gst_Amt]
           ,[Total_Amt]
           ,[Net_Amt]
           ,[Status]
		   ,[Paid_Amt]
		   ,[Balance_Amt])
 values
(
@ID
,@Invoice_No
,@Cust_Name
,@Date_Of_Sell
,@GST_No
,@Total_Items
,@Total_Quantity
,@SGST_Amt
,@CGST_Amt
,@IGST_Amt
,@GST_Amt
,@Total_Amt
,@Net_Amt
,@Status
,@Paid_Amt
,@Balance_Amt
)
End




























GO
/****** Object:  StoredProcedure [dbo].[Insert_TBCutomers_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Insert_TBCutomers_Details]
(
@Customers_ID bigint
,@Customers_Name nvarchar(100)
,@Customers_No nvarchar(100)
,@Customers_City nvarchar(100)
,@Customers_Age nvarchar(100)
,@Customers_Adh_No nvarchar(100)

)
As Begin

insert Student values
(
@Customers_ID
,@Customers_Name
,@Customers_No 
,@Customers_City
,@Customers_Age
,@Customers_Adh_No

)
End






















GO
/****** Object:  StoredProcedure [dbo].[Insert_Transport_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Insert_Transport_Master]
(
@T_InDate nvarchar(100)
,@T_Name nvarchar(100)
,@T_Address nvarchar(100)
,@T_PhoneNo nvarchar(50)
,@T_GSTNo nvarchar(50)
,@T_CPerson nvarchar(50)
,@CP_Phone  nvarchar(50)
,@T_AccountDetails nvarchar(100)
,@T_IFSC_Code nvarchar(50)
)
As Begin

insert into Tb_Transport_Master values
(
@T_InDate
,@T_Name
,@T_Address
,@T_PhoneNo
,@T_GSTNo
,@T_CPerson
,@CP_Phone
,@T_AccountDetails
,@T_IFSC_Code
)
End





















GO
/****** Object:  StoredProcedure [dbo].[Update_Agency_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[Update_Agency_Master]
(
@Com_ID bigint
,@Com_Name nvarchar(50)
,@Com_Depo_Address nvarchar(100)
,@Com_Office_Address nvarchar(100)
,@Com_Phone nvarchar(50)
,@Com_Mobile nvarchar(50)
,@Com_GST_No nvarchar(50)
,@Com_Pan_No nvarchar(50)
,@Com_AccNo nvarchar(50)
,@Com_IFSC nvarchar(50)
,@Com_Bank nvarchar(50)
,@Com_Email nvarchar(50)
,@Com_Website nvarchar(50)
,@logo image
)
As Begin

update Tb_Cylinder_Agency_Master
 set [Com_Office_Address] = @Com_Office_Address
,[Com_Depo_Address] = @Com_Depo_Address
,[Com_Phone] = @Com_Phone
,[Com_Mobile] =   @Com_Mobile
,[Com_GST_No] = @Com_GST_No
,[Com_Pan_No] = @Com_Pan_No
,[Com_AccNo] = @Com_AccNo
,[Com_IFSC] = @Com_IFSC
,[Com_Bank] = @Com_Bank
,[Com_Email] = @Com_Email
,[Com_Website] = @Com_Website
,[Com_Logo] = @logo
where [Com_Name] = @Com_Name
End















GO
/****** Object:  StoredProcedure [dbo].[Update_Asset_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Update_Asset_Master]
(
@Purchase_Date nvarchar(100)
,@Asset_Name nvarchar(50)
,@Purchase_Cost nvarchar(100)
,@Asset_Type nvarchar(50)
)
As Begin

update Tb_Asset_Details
 set [Purchase_Date] = @Purchase_Date
,[Asset_Type] =@Asset_Type
,[Purchase_Cost]=@Purchase_Cost
where [Asset_Name] = @Asset_Name
End





















GO
/****** Object:  StoredProcedure [dbo].[Update_Cust_DC_Content]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[Update_Cust_DC_Content]
(
@dc_no nvarchar(50)
,@cy_type nvarchar(50)
,@particular nvarchar(50)
,@sr_no nvarchar(50)
,@item_no nvarchar(50)
,@quantity nvarchar(50)
,@rate nvarchar(50) 
,@unit nvarchar(50)
,@total nvarchar(50)
,@sell_status nvarchar(50)
,@received_status nvarchar(50)
,@cust_dc_no nvarchar(50)
)
As Begin

update Tb_CDC_Content_Details set

[Cy_Type]=@cy_type
,[Sr_No]=@sr_no
,[Quantity]=@quantity
,[Rate]=@rate
,[Unit]=@unit
,[Total]=@total
,[Sell_Status] = @sell_status
,[Receive_Status] = @received_status
,[Cust_Dc_No]=@cust_dc_no
WHERE [Particulars]=@particular AND [C_Dc_No]=@dc_no AND [Item_No]=@item_no
End

























GO
/****** Object:  StoredProcedure [dbo].[Update_Cust_DC_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE procedure [dbo].[Update_Cust_DC_Details]
(
@dc_date nvarchar(50)
,@dc_no nvarchar(50)
,@cust_name nvarchar(50)
,@total_amt nvarchar(50)
,@paid_amt nvarchar(50)
,@remark nvarchar(200)
,@total_items nvarchar(50)
,@balance_amt nvarchar(50)
,@pay_mode nvarchar(50)
,@reference nvarchar(50)  
,@deposit_Amt nvarchar(50)
,@deposit_Return nvarchar(50)
,@for_Days nvarchar(50)
,@reason nvarchar(50)
,@Vehicle_No varchar(50)
)
As Begin

update Tb_CDC_Details set

[C_Dc_Date]=@dc_date
,[C_Name]=@cust_name
,[Total_Amt]=@total_amt
,[Paid_Amt]=[Paid_Amt] + @paid_amt
,[Remark]=@remark
,[Total_Items]=@total_items
,[Balance_Amt]=@balance_amt
,[Pay_Mode]=@pay_mode
,[Pay_Reference]=@reference 
,[Deposit_Amt]=@deposit_Amt
,[Deposit_Return]=@deposit_Return
,[For_Days]=@for_Days
,[Reason]=@reason
,[Vehicle_No]=@Vehicle_No
WHERE [C_Dc_No] = @dc_no
End
























GO
/****** Object:  StoredProcedure [dbo].[Update_Customer_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[Update_Customer_Master]
(
@C_InDate nvarchar(100)
,@Cust_Name nvarchar(50)
,@Cust_Address nvarchar(100)
,@Cust_PhoneNo nvarchar(50)
,@Cust_GSTNo nvarchar(50)
,@Cust_CPerson nvarchar(50)
,@CP_PhoneNo nvarchar(50)
)
As Begin

update Tb_Customer_Master
 set [C_InDate] = @C_InDate
,[Cust_Address] = @Cust_Address
,[Cust_PhoneNo] = @Cust_PhoneNo
,[Cust_GSTNo] =   @Cust_GSTNo
,[Cust_CPerson] = @Cust_CPerson
,[CP_PhoneNo] = @CP_PhoneNo
where [Cust_Name] = @Cust_Name
End
























GO
/****** Object:  StoredProcedure [dbo].[Update_Expenses_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[Update_Expenses_Details]
(
@ID BIGINT
,@Ex_Date	nvarchar(50)	
,@Ex_Type	nvarchar(50)	
,@Vehicle_No	nvarchar(50)	
,@Person_Name	nvarchar(50)	
,@Ex_Amount	nvarchar(50)	
,@Ex_Description	nvarchar(50)	
,@Wages_Type	nvarchar(50)
)
As Begin

UPDATE Tb_Expenses_Master SET

Ex_Date=@Ex_Date
,Ex_Type=@Ex_Type
,Vehicle_No=@Vehicle_No
,Person_Name=@Person_Name
,Ex_Amount=@Ex_Amount
,Ex_Description=@Ex_Description
,Wages_Type=@Wages_Type
WHERE Exp_ID = @ID
End




















GO
/****** Object:  StoredProcedure [dbo].[Update_Fill_Content_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Update_Fill_Content_Details]
(
@Item_No nvarchar(100)
,@Sr_No nvarchar(100)
,@Name nvarchar(50)
,@Fill_ID nvarchar(50)
,@Rate nvarchar(50)
,@Status nvarchar(50)
)
As Begin

update Tb_Fill_Content_Master
 set [Sr_No] = @Sr_No
,[Name] =@Name
,[Rate]=@Rate

where [Fill_ID] = @Fill_ID and [Item_No] = @Item_No
End





















GO
/****** Object:  StoredProcedure [dbo].[Update_Fill_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Update_Fill_Details]
(
@Fill_Date nvarchar(100)
,@count nvarchar(50)
,@Fill_ID nvarchar(50)
,@net_amt nvarchar(50)
,@paid_amt nvarchar(50)
,@balance_amt nvarchar(50)
,@paid_by nvarchar(50)
,@reference nvarchar(50)
,@supp_invoice_no nvarchar(50)
,@Vehicle_No varchar(50)
)
As Begin

UPDATE [dbo].[Tb_Fill_Master]
   SET 
      [Fill_Date] = @Fill_Date
      ,[Cylinder_Count] = @count
      ,[Net_Amt] = @net_amt
	  ,[Paid_Amt]= @paid_amt
	  ,[Balance_Amt] = @balance_amt
	  ,[Paid_By] = @paid_by
	  ,[Reference] = @reference
	  ,[Supp_Invoice_No] = @supp_invoice_no
	  ,[Vehicle_No]=@Vehicle_No
where [Fill_ID] = @Fill_ID
End




















GO
/****** Object:  StoredProcedure [dbo].[Update_Purchase_Content_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[Update_Purchase_Content_Details]
(
@Particulers nvarchar(100)
,@HSN nvarchar(50)
,@Rate nvarchar(50)
,@Disc nvarchar(50)
,@Quantity nvarchar(50), @Unit nvarchar(50)
,@Total nvarchar(100)
,@SGST nvarchar(50)
,@CGST nvarchar(50)
,@IGST nvarchar(50)
,@TaxableAmt nvarchar(50)
,@Purchase_Type nvarchar(100)
,@Sr_No nvarchar(50)
,@Part_No nvarchar(50)
,@cylinder_Type nvarchar(50)
,@invoice_No nvarchar(50)
,@Cust_Supp_Name nvarchar(50)
)
As Begin

UPDATE Tb_Purchase_Content_Master set 

[HSN] = @HSN
,[Rate] = @Rate
,[Disc] = @Disc
,[Quantity] = @Quantity
,[Unit] = @Unit
,[Total] = @Total
,[SGST] = @SGST
,[CGST] = @CGST
,[IGST] = @IGST
,[TaxableAmt] = @TaxableAmt
,[Purchase_Type] = @Purchase_Type
,[Part_No] = @Part_No
,[Cylinder_Type] = @cylinder_Type
where [Invoice_No] = @Invoice_No AND [Particulers] = @Particulers AND [Sr_No] = @Sr_No and [Cust_Supp_Name]=@Cust_Supp_Name
End



























GO
/****** Object:  StoredProcedure [dbo].[Update_Purchase_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[Update_Purchase_Details]
(
@Invoice_No nvarchar(100),
@Supplier_Name nvarchar(100)
,@SGST_Per nvarchar(50)
,@SGST_Amt nvarchar(50)
,@CGST_Per nvarchar(50)
,@CGST_Amt nvarchar(50)
,@IGST_Per nvarchar(50)
,@IGST_Amt nvarchar(50)
,@total_items nvarchar(50)
,@Total_Amt nvarchar(50)
,@GST_Amt nvarchar(50)
,@Net_Amt nvarchar(50)
,@Paid_Amt nvarchar(50)
,@Balance_Amt nvarchar(50)
,@Payment_Mode nvarchar(50)
,@Reff_No nvarchar(50)
)
As Begin

update Tb_Purchase
 set [SGST_Per] = @SGST_Per

,[SGST_Amt] = @SGST_Amt
,[CGST_Per] = @CGST_Per
,[CGST_Amt] = @CGST_Amt
,[IGST_Per] = @IGST_Per
,[IGST_Amt] = @IGST_Amt
,[Total_Items]=@total_items
,[Total_Amt] =  @Total_Amt
,[GST_Amt] = @GST_Amt
,[Net_Amt] = @Net_Amt
,[Paid_Amt] = @Paid_Amt
,[Balance_Amt] = @Balance_Amt
,[Payment_Mode] = @Payment_Mode
,[Reff_No] = @Reff_No
where [Invoice_No] = @Invoice_No and [Supplier_Name]=@Supplier_Name
End




























GO
/****** Object:  StoredProcedure [dbo].[Update_Staff_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Update_Staff_Details]
(
@St_Name nvarchar(50)
,@St_Address nvarchar(100)
,@St_Phone nvarchar(50)
,@St_Mobile nvarchar(50)
,@St_Aadhar_No nvarchar(50)
,@StEmContact nvarchar(50)
,@St_JoinDate nvarchar(50)
,@St_Designation nvarchar(50)
,@St_Salary nvarchar(50)
,@w_days nvarchar(50)
,@St_WHours nvarchar(50)
,@w_min nvarchar(50)
,@Per_Day_Salary nvarchar(50)
,@Per_Hour_Salary nvarchar(50)
,@St_ResignDate nvarchar(50)
,@St_Reason nvarchar(100)
,@Date_of_Birth nvarchar(100)
,@Account_No nvarchar(50)
,@IFSC nvarchar(50)
,@Bank nvarchar(50)
,@Current_Age nvarchar(50)
,@status nvarchar(50)
,@Staff_ID bigint
)
As Begin

update Tb_Staff_Details
 set [St_Name] = @St_Name
,[St_Address] = @St_Address
,[St_Phone] = @St_Phone
,[St_Mobile] =   @St_Mobile
,[St_Aadhar_No] = @St_Aadhar_No
,[St_EmContact] = @StEmContact
,[St_JoinDate] = @St_JoinDate
,[St_Designation] = @St_Designation
,[St_Salary] = @St_Salary
,[St_WDays] = @w_days
,[St_WHours] = @St_WHours
,[St_WMin] = @w_min
,[Per_Day_Salary] = @Per_Day_Salary
,[Per_Hour_Salary] = @Per_Hour_Salary
,[St_ResignDate] = @St_ResignDate
,[St_Reason] = @St_Reason
,[Date_of_Birth] = @Date_of_Birth
,[Account_No] = @Account_No
,[IFSC] = @IFSC
,[Bank] = @Bank
,[Current_Age] = @Current_Age
,[Staff_Status] = @status

where [Staff_ID] = @Staff_ID
End





















GO
/****** Object:  StoredProcedure [dbo].[Update_Supplier_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Update_Supplier_Master]
(
@Supp_InDate nvarchar(100)
,@Supp_CompName nvarchar(50)
,@Supp_Address nvarchar(100)
,@Supp_Phone nvarchar(50)
,@Supp_GST nvarchar(50)
,@Supp_ConPerson nvarchar(50)
,@Supp_CPersonNo nvarchar(50)
,@Supp_AccNo nvarchar(50)
,@Supp_IFSC nvarchar(50)
,@Supp_Bank nvarchar(50)
)
As Begin

update Tb_Supplier_Master
 set [Supp_InDate] = @Supp_InDate
,[Supp_Address] = @Supp_Address
,[Supp_Phone] = @Supp_Phone
,[Supp_GST] =   @Supp_GST
,[Supp_ConPerson] = @Supp_ConPerson
,[Supp_CPersonNo] = @Supp_CPersonNo
,[Supp_AccNo] = @Supp_AccNo
,[Supp_IFSC] = @Supp_IFSC
,[Supp_Bank] = @Supp_Bank
where [Supp_CompName] = @Supp_CompName
End





















GO
/****** Object:  StoredProcedure [dbo].[Update_Supplier_Material]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Update_Supplier_Material]
(
@Cylinder_Type nvarchar(50)
,@Particulers nvarchar(50)
,@Rate nvarchar(50)
,@Rate_For nvarchar(50)
,@Revised_Date nvarchar(50)
,@Supp_Name nvarchar(50)
)
As Begin

update Tb_Supplier_Material_Master
 set [Cylinder_Type] = @Cylinder_Type
,[Rate] = @Rate
,[Rate_For] = @Rate_For
,[Revised_Date] = @Revised_Date
where Supp_Name = @Supp_Name AND [Particulers] = @Particulers
End





















GO
/****** Object:  StoredProcedure [dbo].[Update_Tax_Sell_Content_Particular]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[Update_Tax_Sell_Content_Particular]
(
@Particulers nvarchar(100)
,@HSN nvarchar(50)
,@Rate nvarchar(50)
,@Disc nvarchar(50)
,@Quantity nvarchar(50)
,@Unit nvarchar(50)
,@Total nvarchar(100)
,@SGST nvarchar(50)
,@CGST nvarchar(50)
,@IGST nvarchar(50)
,@TaxableAmt nvarchar(50)
,@Sr_No nvarchar(50)
,@Part_No nvarchar(50)
,@Gas_Type nvarchar(50)
,@invoice_No nvarchar(50)
,@Status nvarchar(50)
)
As Begin

UPDATE Tb_Tax_Sell_Content_Particular set 

[HSN] = @HSN
,[Rate] = @Rate
,[Discount] = @Disc
,[Quantity] = @Quantity
,[Unit] = @Unit
,[Total] = @Total
,[SGST] = @SGST
,[CGST] = @CGST
,[IGST] = @IGST
,[Taxable_Amt] = @TaxableAmt
,[Gas_Type] = @Gas_Type
,[Sell_Status] =@Status
where [Invoice_No] = @Invoice_No AND [Particulers] = @Particulers AND Item_No = @Part_No
End



























GO
/****** Object:  StoredProcedure [dbo].[Update_Tax_Sell_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[Update_Tax_Sell_Details]
(
@Invoice_No nvarchar(100)
,@Total_Items nvarchar(50)
,@Total_Quantity nvarchar(50)
,@SGST_Amt nvarchar(50)
,@CGST_Amt nvarchar(50)
,@IGST_Amt nvarchar(50)
,@GST_Amt nvarchar(50)
,@Total_Amt nvarchar(50)
,@Net_Amt nvarchar(50)
--,@Status nvarchar(50)
)
As Begin

update Tb_Tax_Sell_Details set
[Total_Items]=@Total_Items
           ,[Total_Quantity]=@Total_Quantity
           ,[Sgst_Amt]=@SGST_Amt
           ,[Cgst_Amt]=@CGST_Amt
           ,[Igst_Amt]=@IGST_Amt
           ,[Gst_Amt]=@GST_Amt
           ,[Total_Amt]=@Total_Amt           
		   ,[Net_Amt]=@Net_Amt
         --  ,[Status]=@Status
		   where @Invoice_No= [Invoice_No]


End




























GO
/****** Object:  StoredProcedure [dbo].[Update_Transport_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[Update_Transport_Master]
(
@T_InDate nvarchar(100)
,@T_Name nvarchar(100)
,@T_Address nvarchar(100)
,@T_PhoneNo nvarchar(50)
,@T_GSTNo nvarchar(50)
,@T_CPerson nvarchar(50)
,@CP_Phone  nvarchar(50)
,@T_AccountDetails nvarchar(100)
,@T_IFSC_Code nvarchar(50)
)
As Begin

update Tb_Transport_Master
 set [T_InDate] = @T_InDate
,[T_Address] = @T_Address
,[T_PhoneNo] = @T_PhoneNo
,[T_GSTNo] =   @T_GSTNo
,[T_CPerson] = @T_CPerson
,[CP_Phone] = @CP_Phone
,[T_AccountDetails] = @T_AccountDetails
,[T_IFSC_Code] = @T_IFSC_Code
where [T_Name] = @T_Name
End





















GO
/****** Object:  Table [dbo].[Tb_Asset_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Asset_Details](
	[Purchase_Date] [nvarchar](100) NULL,
	[Asset_Name] [nvarchar](50) NULL,
	[Purchase_Cost] [nvarchar](100) NULL,
	[Asset_Type] [nvarchar](50) NULL,
	[Asset_ID] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Tb_Asset_Detail] PRIMARY KEY CLUSTERED 
(
	[Asset_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_CDC_Content_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_CDC_Content_Details](
	[Item_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[C_Dc_No] [nvarchar](50) NULL,
	[Cy_Type] [nvarchar](50) NULL,
	[Particulars] [nvarchar](50) NULL,
	[Sr_No] [nvarchar](50) NULL,
	[Item_No] [nvarchar](50) NULL,
	[Quantity] [nvarchar](50) NULL,
	[Rate] [nvarchar](50) NULL,
	[Unit] [nvarchar](50) NULL,
	[Total] [nvarchar](50) NULL,
	[Sell_Status] [nvarchar](50) NULL,
	[Receive_Status] [nvarchar](50) NULL,
	[Cust_Dc_No] [nvarchar](50) NULL,
	[Received_Date] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tb_CDC_Content_Details] PRIMARY KEY CLUSTERED 
(
	[Item_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_CDC_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_CDC_Details](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[C_Dc_Date] [nvarchar](50) NULL,
	[C_Dc_No] [nvarchar](50) NULL,
	[C_Name] [nvarchar](50) NULL,
	[Total_Items] [nvarchar](50) NULL,
	[Total_Amt] [nvarchar](50) NULL,
	[Paid_Amt] [nvarchar](50) NULL,
	[Balance_Amt] [nvarchar](50) NULL,
	[Remark] [nvarchar](200) NULL,
	[Pay_Mode] [nvarchar](50) NULL,
	[Pay_Reference] [nvarchar](50) NULL,
	[Status] [nvarchar](50) NULL,
	[Deposit_Amt] [nvarchar](50) NULL,
	[Deposit_Return] [nvarchar](50) NULL,
	[For_Days] [nvarchar](50) NULL,
	[Reason] [nvarchar](50) NULL,
	[Tax_Status] [nvarchar](50) NULL,
	[C_ID] [bigint] NULL,
	[Vehicle_No] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tb_CDC_Detail_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Customer_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Customer_Master](
	[C_InDate] [nvarchar](100) NULL,
	[Cust_Name] [nvarchar](100) NULL,
	[Cust_Address] [nvarchar](100) NULL,
	[Cust_PhoneNo] [nvarchar](50) NULL,
	[Cust_GSTNo] [nvarchar](50) NULL,
	[Cust_CPerson] [nvarchar](100) NULL,
	[CP_PhoneNo] [nvarchar](50) NULL,
	[Cust_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Cust_Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tb_Customer_Master] PRIMARY KEY CLUSTERED 
(
	[Cust_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Cylinder_Agency_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Cylinder_Agency_Master](
	[Com_ID] [bigint] NOT NULL,
	[Com_Name] [nvarchar](50) NULL,
	[Com_Depo_Address] [nvarchar](100) NULL,
	[Com_Office_Address] [nvarchar](100) NULL,
	[Com_Phone] [nvarchar](50) NULL,
	[Com_Mobile] [nvarchar](50) NULL,
	[Com_GST_No] [nvarchar](50) NULL,
	[Com_Pan_No] [nvarchar](50) NULL,
	[Com_AccNo] [nvarchar](50) NULL,
	[Com_IFSC] [nvarchar](50) NULL,
	[Com_Logo] [image] NULL,
	[Com_Bank] [nvarchar](50) NULL,
	[Com_Email] [nvarchar](50) NULL,
	[Com_Website] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Cylinder_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Cylinder_Master](
	[C_ID] [bigint] NULL,
	[C_P_No] [bigint] NULL,
	[C_Name] [nvarchar](100) NULL,
	[C_Rate] [bigint] NULL,
	[C_Type] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Expenses_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Expenses_Master](
	[Exp_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Ex_Date] [nvarchar](50) NULL,
	[Ex_Type] [nvarchar](50) NULL,
	[Vehicle_No] [nvarchar](50) NULL,
	[Person_Name] [nvarchar](50) NULL,
	[Ex_Amount] [nvarchar](50) NULL,
	[Ex_Description] [nvarchar](50) NULL,
	[Wages_Type] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tb_Expenses_Master] PRIMARY KEY CLUSTERED 
(
	[Exp_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Fill_Content_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Fill_Content_Master](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Item_No] [nvarchar](50) NOT NULL,
	[Sr_No] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Fill_ID] [nvarchar](50) NULL,
	[Rate] [nvarchar](50) NULL,
	[Status] [nvarchar](50) NULL,
	[Supp_Dc_No] [nvarchar](50) NULL,
	[Received_Date] [nvarchar](50) NULL,
	[Cy_Type] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tb_Fill_Content_Master] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Fill_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Fill_Master](
	[T_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ID] [bigint] NULL,
	[Fill_Date] [nvarchar](50) NULL,
	[Supplier_Name] [nvarchar](50) NULL,
	[Cylinder_Count] [bigint] NULL,
	[Fill_ID] [nvarchar](50) NULL,
	[Net_Amt] [nvarchar](50) NULL,
	[Cancel_status] [nvarchar](50) NULL,
	[Paid_Amt] [nvarchar](50) NULL,
	[Balance_Amt] [nvarchar](50) NULL,
	[Paid_By] [nvarchar](50) NULL,
	[Reference] [nvarchar](50) NULL,
	[Supp_Invoice_No] [nvarchar](50) NULL,
	[Vehicle_No] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tb_Fill_Maste_1] PRIMARY KEY CLUSTERED 
(
	[T_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Filling_Payment]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Filling_Payment](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Fill_DC_No] [nvarchar](50) NULL,
	[Payment_Date] [nvarchar](50) NULL,
	[Invoice_Amt] [nvarchar](50) NULL,
	[Paid_Amt] [nvarchar](50) NULL,
	[Payment_Mode] [nvarchar](50) NULL,
	[Reference] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tb_Filling_Payment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Inventory_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Inventory_Master](
	[Particulars] [nvarchar](100) NULL,
	[Type] [nvarchar](50) NULL,
	[In_Stock] [nvarchar](50) NULL,
	[Out_Stock] [nvarchar](50) NULL,
	[Cylinder_Type] [nvarchar](50) NULL,
	[Inventory_ID] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Tb_Inventory_Master] PRIMARY KEY CLUSTERED 
(
	[Inventory_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Payment_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Payment_Master](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Cust_Invoice_No] [nvarchar](50) NULL,
	[Supp_Invoice_No] [nvarchar](50) NULL,
	[Payment_Date] [nvarchar](50) NULL,
	[Invoice_Amt] [nvarchar](50) NULL,
	[Paid_Amt] [nvarchar](50) NULL,
	[Payment_Mode] [nvarchar](50) NULL,
	[Reference] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tb_Payment_Maste] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Purchase]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tb_Purchase](
	[Purchase_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Supplier_Name] [nvarchar](100) NULL,
	[Date_Of_Purchase] [varchar](50) NULL,
	[Invoice_No] [nvarchar](100) NULL,
	[Gst_No] [nvarchar](100) NULL,
	[SGST_Per] [nvarchar](50) NULL,
	[SGST_Amt] [nvarchar](50) NULL,
	[CGST_Per] [nvarchar](50) NULL,
	[CGST_Amt] [nvarchar](50) NULL,
	[IGST_Per] [nvarchar](50) NULL,
	[IGST_Amt] [nvarchar](50) NULL,
	[Total_Items] [nvarchar](50) NULL,
	[Total_Amt] [nvarchar](50) NULL,
	[GST_Amt] [nvarchar](50) NULL,
	[Net_Amt] [nvarchar](50) NULL,
	[Paid_Amt] [nvarchar](50) NULL,
	[Balance_Amt] [nvarchar](50) NULL,
	[Payment_Mode] [nvarchar](50) NULL,
	[Reff_No] [nvarchar](50) NULL,
	[Gst_Percent] [nvarchar](50) NULL,
	[Cancel_Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tb_Purchas] PRIMARY KEY CLUSTERED 
(
	[Purchase_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_Purchase_Content_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Purchase_Content_Master](
	[Particulers] [nvarchar](100) NULL,
	[HSN] [nvarchar](50) NULL,
	[Rate] [nvarchar](50) NULL,
	[Disc] [nvarchar](50) NULL,
	[Quantity] [nvarchar](50) NULL,
	[Unit] [nvarchar](50) NULL,
	[Total] [nvarchar](100) NULL,
	[SGST] [nvarchar](50) NULL,
	[CGST] [nvarchar](50) NULL,
	[IGST] [nvarchar](50) NULL,
	[TaxableAmt] [nvarchar](50) NULL,
	[Purchase_Type] [nvarchar](100) NULL,
	[Sr_No] [nvarchar](50) NULL,
	[Part_No] [nvarchar](50) NULL,
	[Cylinder_Type] [nvarchar](50) NULL,
	[Invoice_No] [nvarchar](50) NULL,
	[Purchase_Con_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Cylinder_Status] [nvarchar](50) NULL,
	[Cust_Supp_Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tb_Purchase_Content_Master] PRIMARY KEY CLUSTERED 
(
	[Purchase_Con_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_SignUp_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_SignUp_Details](
	[SignUp_Date] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[Designation] [nvarchar](50) NULL,
	[Contact_No] [nvarchar](50) NULL,
	[User_Type] [nvarchar](50) NULL,
	[User_ID] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Confirm_Password] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Staff_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Staff_Details](
	[St_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[St_Name] [nvarchar](50) NULL,
	[St_Address] [nvarchar](100) NULL,
	[St_Phone] [nvarchar](50) NULL,
	[St_Mobile] [nvarchar](50) NULL,
	[St_Aadhar_No] [nvarchar](50) NULL,
	[St_EmContact] [nvarchar](50) NULL,
	[St_JoinDate] [nvarchar](50) NULL,
	[St_Designation] [nvarchar](50) NULL,
	[St_Salary] [nvarchar](50) NULL,
	[St_WDays] [nvarchar](50) NULL,
	[St_WHours] [nvarchar](50) NULL,
	[St_WMin] [nvarchar](50) NULL,
	[Per_Day_Salary] [nvarchar](50) NULL,
	[Per_Hour_Salary] [nvarchar](50) NULL,
	[St_ResignDate] [nvarchar](50) NULL,
	[St_Reason] [nvarchar](100) NULL,
	[Date_of_Birth] [nvarchar](100) NULL,
	[Account_No] [nvarchar](50) NULL,
	[IFSC] [nvarchar](50) NULL,
	[Bank] [nvarchar](50) NULL,
	[Current_Age] [nvarchar](50) NULL,
	[Staff_Status] [nvarchar](50) NULL,
	[Staff_ID] [bigint] NULL,
 CONSTRAINT [PK_Tb_Staff_Detail] PRIMARY KEY CLUSTERED 
(
	[St_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Supplier_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Supplier_Master](
	[Supp_InDate] [nvarchar](100) NULL,
	[Supp_CompName] [nvarchar](100) NULL,
	[Supp_Address] [nvarchar](100) NULL,
	[Supp_Phone] [nvarchar](50) NULL,
	[Supp_GST] [nvarchar](50) NULL,
	[Supp_ConPerson] [nvarchar](50) NULL,
	[Supp_CPersonNo] [nvarchar](50) NULL,
	[Supp_AccNo] [nvarchar](50) NULL,
	[Supp_IFSC] [nvarchar](50) NULL,
	[Supp_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](50) NULL,
	[Supp_Bank] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tb_Supplier_Master] PRIMARY KEY CLUSTERED 
(
	[Supp_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Supplier_Material_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Supplier_Material_Master](
	[Supp_Content_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Cylinder_Type] [nvarchar](50) NULL,
	[Particulers] [nvarchar](50) NULL,
	[Rate] [nvarchar](50) NULL,
	[Rate_For] [nvarchar](50) NULL,
	[Revised_Date] [nvarchar](50) NULL,
	[Supp_Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tb_Supplier_Material_Maste] PRIMARY KEY CLUSTERED 
(
	[Supp_Content_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Tax_Content_DC]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Tax_Content_DC](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[Invoice_No] [nvarchar](50) NULL,
	[C_Dc_No] [nvarchar](50) NULL,
	[C_Dc_Date] [nvarchar](50) NULL,
	[Total_Items] [nvarchar](50) NULL,
	[Total_Amt] [nvarchar](50) NULL,
	[Paid_Amt] [nvarchar](50) NULL,
	[column1] [nvarchar](50) NULL,
	[column2] [nvarchar](50) NULL,
	[column3] [nvarchar](50) NULL,
	[column4] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tb_Tax_Content_Dcs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Tax_Sell_Content_Particular]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Tax_Sell_Content_Particular](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[Invoice_No] [nvarchar](50) NULL,
	[Gas_Type] [nvarchar](50) NULL,
	[Particulers] [nvarchar](100) NULL,
	[HSN] [nvarchar](50) NULL,
	[Item_No] [nvarchar](50) NULL,
	[Sr_No] [nvarchar](50) NULL,
	[Rate] [nvarchar](50) NULL,
	[Discount] [nvarchar](50) NULL,
	[Quantity] [nvarchar](50) NULL,
	[Unit] [nvarchar](50) NULL,
	[Total] [nvarchar](100) NULL,
	[SGST] [nvarchar](50) NULL,
	[CGST] [nvarchar](50) NULL,
	[IGST] [nvarchar](50) NULL,
	[Taxable_Amt] [nvarchar](50) NULL,
	[Sell_Status] [nchar](10) NULL,
	[column2] [nchar](10) NULL,
	[column3] [nchar](10) NULL,
 CONSTRAINT [PK_Tb_Sell_Content_Details] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Tax_Sell_Details]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Tax_Sell_Details](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[invoice_id] [bigint] NULL,
	[Invoice_No] [nvarchar](50) NULL,
	[Cust_Name] [nvarchar](50) NULL,
	[Sell_Date] [nvarchar](50) NULL,
	[Gst_No] [nvarchar](50) NULL,
	[Total_Items] [nvarchar](50) NULL,
	[Total_Quantity] [nvarchar](50) NULL,
	[Sgst_Amt] [nvarchar](50) NULL,
	[Cgst_Amt] [nvarchar](50) NULL,
	[Igst_Amt] [nvarchar](50) NULL,
	[Gst_Amt] [nvarchar](50) NULL,
	[Total_Amt] [nvarchar](50) NULL,
	[Net_Amt] [nvarchar](50) NULL,
	[Status] [nvarchar](50) NULL,
	[Paid_Amt] [nvarchar](50) NULL,
	[Balance_Amt] [nvarchar](50) NULL,
	[column1] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tb_Tax_Sell_Detail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Transport_Master]    Script Date: 03-04-2022 13:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Transport_Master](
	[T_InDate] [nvarchar](100) NULL,
	[T_Name] [nvarchar](100) NULL,
	[T_Address] [nvarchar](100) NULL,
	[T_PhoneNo] [nvarchar](50) NULL,
	[T_GSTNo] [nvarchar](50) NULL,
	[T_CPerson] [nvarchar](50) NULL,
	[CP_Phone] [nvarchar](50) NULL,
	[T_AccountDetails] [nvarchar](100) NULL,
	[T_IFSC_Code] [nvarchar](50) NULL,
	[Transport_ID] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Tb_Transport_Master] PRIMARY KEY CLUSTERED 
(
	[Transport_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [master]
GO
ALTER DATABASE [New_Cylinder_Final_Updated_2021] SET  READ_WRITE 
GO
