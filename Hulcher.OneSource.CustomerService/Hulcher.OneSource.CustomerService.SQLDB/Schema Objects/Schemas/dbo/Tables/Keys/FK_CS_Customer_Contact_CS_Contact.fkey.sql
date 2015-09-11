ALTER TABLE [dbo].[CS_Customer_Contact]  WITH CHECK ADD  CONSTRAINT [FK_CS_Customer_Contact_CS_Contact] FOREIGN KEY([ContactID])
REFERENCES [dbo].[CS_Contact] ([ID])


GO
ALTER TABLE [dbo].[CS_Customer_Contact] CHECK CONSTRAINT [FK_CS_Customer_Contact_CS_Contact]

