CREATE TABLE [dbo].[CS_CustomerInfo](
	[JobId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[EicContactId] [int] NOT NULL,
	[AdditionalContactId] [int] NOT NULL,
	[BillToContactId] [int] NOT NULL,
	[InitialCustomerContactId] [int] NOT NULL,
	[DivisionId] [int] NOT NULL,
	[PocEmployeeId] [int] NOT NULL,
	[IsCustomer] [bit] NOT NULL,
	[CreatedBy] [varchar](100) NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ModifiedBy] [varchar](100) NOT NULL,
	[ModificationDate] [date] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_CustomerInfo] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


