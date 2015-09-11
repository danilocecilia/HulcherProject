ALTER TABLE [dbo].[CS_CallType]  WITH CHECK ADD  CONSTRAINT [FK_CS_CallType_CS_PrimaryCallType] FOREIGN KEY([PrimayCallTypeID])
REFERENCES [dbo].[CS_PrimaryCallType] ([ID])


GO
ALTER TABLE [dbo].[CS_CallType] CHECK CONSTRAINT [FK_CS_CallType_CS_PrimaryCallType]

