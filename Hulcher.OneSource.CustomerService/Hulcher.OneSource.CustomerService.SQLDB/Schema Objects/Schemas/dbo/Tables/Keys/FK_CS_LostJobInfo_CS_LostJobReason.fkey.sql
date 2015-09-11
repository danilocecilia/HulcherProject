ALTER TABLE [dbo].[CS_LostJobInfo]  WITH CHECK ADD  CONSTRAINT [FK_CS_LostJobInfo_CS_LostJobReason] FOREIGN KEY([ReasonID])
REFERENCES [dbo].[CS_LostJobReason] ([ID])


GO
ALTER TABLE [dbo].[CS_LostJobInfo] CHECK CONSTRAINT [FK_CS_LostJobInfo_CS_LostJobReason]

