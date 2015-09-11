ALTER TABLE [dbo].[CS_JobInfo]  WITH CHECK ADD  CONSTRAINT [FK_CS_JobInfo_CS_PresetInfo] FOREIGN KEY([PresetInfoID])
REFERENCES [dbo].[CS_PresetInfo] ([ID])


GO
ALTER TABLE [dbo].[CS_JobInfo] CHECK CONSTRAINT [FK_CS_JobInfo_CS_PresetInfo]

