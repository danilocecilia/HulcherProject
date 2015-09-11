ALTER TABLE [dbo].[CS_CustomerInfo]  WITH CHECK ADD  CONSTRAINT [FK_CS_CustomerInfo_CS_Job] FOREIGN KEY([JobId])
REFERENCES [dbo].[CS_Job] ([ID])


GO
ALTER TABLE [dbo].[CS_CustomerInfo] CHECK CONSTRAINT [FK_CS_CustomerInfo_CS_Job]

