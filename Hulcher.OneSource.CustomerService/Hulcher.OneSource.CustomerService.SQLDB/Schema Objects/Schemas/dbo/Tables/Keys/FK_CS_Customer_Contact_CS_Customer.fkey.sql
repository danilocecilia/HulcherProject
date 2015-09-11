ALTER TABLE [dbo].[CS_Customer_Contact]  WITH CHECK ADD  CONSTRAINT [FK_CS_Customer_Contact_CS_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[CS_Customer] ([ID])


GO
ALTER TABLE [dbo].[CS_Customer_Contact] CHECK CONSTRAINT [FK_CS_Customer_Contact_CS_Customer]

