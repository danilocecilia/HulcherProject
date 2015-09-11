ALTER TABLE [dbo].[CS_JobInfo]  WITH CHECK ADD  CONSTRAINT [FK_CS_JobInfo_CS_JobStatus] FOREIGN KEY([JobStatusID])
REFERENCES [dbo].[CS_JobStatus] ([ID])


GO
ALTER TABLE [dbo].[CS_JobInfo] CHECK CONSTRAINT [FK_CS_JobInfo_CS_JobStatus]

