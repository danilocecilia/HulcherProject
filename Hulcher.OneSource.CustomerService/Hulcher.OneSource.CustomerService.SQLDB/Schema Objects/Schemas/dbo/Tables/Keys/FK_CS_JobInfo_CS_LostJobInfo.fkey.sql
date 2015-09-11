ALTER TABLE [dbo].[CS_JobInfo]  WITH CHECK ADD  CONSTRAINT [FK_CS_JobInfo_CS_LostJobInfo] FOREIGN KEY([LostJobInfo])
REFERENCES [dbo].[CS_LostJobInfo] ([ID])


GO
ALTER TABLE [dbo].[CS_JobInfo] CHECK CONSTRAINT [FK_CS_JobInfo_CS_LostJobInfo]

