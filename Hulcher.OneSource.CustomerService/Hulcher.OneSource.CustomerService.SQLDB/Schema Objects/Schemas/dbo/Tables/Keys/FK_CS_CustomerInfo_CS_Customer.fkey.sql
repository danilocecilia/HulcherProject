ALTER TABLE [dbo].[CS_CustomerInfo]  WITH CHECK ADD  CONSTRAINT [FK_CS_CustomerInfo_CS_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[CS_Customer] ([ID])


GO
ALTER TABLE [dbo].[CS_CustomerInfo] CHECK CONSTRAINT [FK_CS_CustomerInfo_CS_Customer]

