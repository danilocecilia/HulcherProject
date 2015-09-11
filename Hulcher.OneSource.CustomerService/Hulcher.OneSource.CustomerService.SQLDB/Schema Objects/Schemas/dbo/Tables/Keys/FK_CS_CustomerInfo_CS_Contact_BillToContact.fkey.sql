ALTER TABLE [dbo].[CS_CustomerInfo]  WITH CHECK ADD  CONSTRAINT [FK_CS_CustomerInfo_CS_Contact_BillToContact] FOREIGN KEY([BillToContactId])
REFERENCES [dbo].[CS_Contact] ([ID])


GO
ALTER TABLE [dbo].[CS_CustomerInfo] CHECK CONSTRAINT [FK_CS_CustomerInfo_CS_Contact_BillToContact]

