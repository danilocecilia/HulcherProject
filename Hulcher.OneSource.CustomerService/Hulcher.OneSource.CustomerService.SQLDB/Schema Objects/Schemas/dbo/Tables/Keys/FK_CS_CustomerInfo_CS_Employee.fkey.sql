ALTER TABLE [dbo].[CS_CustomerInfo]  WITH CHECK ADD  CONSTRAINT [FK_CS_CustomerInfo_CS_Employee] FOREIGN KEY([PocEmployeeId])
REFERENCES [dbo].[CS_Employee] ([ID])


GO
ALTER TABLE [dbo].[CS_CustomerInfo] CHECK CONSTRAINT [FK_CS_CustomerInfo_CS_Employee]

