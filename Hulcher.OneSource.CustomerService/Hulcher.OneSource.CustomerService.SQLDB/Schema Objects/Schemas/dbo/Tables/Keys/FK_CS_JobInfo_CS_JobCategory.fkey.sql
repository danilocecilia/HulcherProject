ALTER TABLE [dbo].[CS_JobInfo]  WITH CHECK ADD  CONSTRAINT [FK_CS_JobInfo_CS_JobCategory] FOREIGN KEY([JobCategoryID])
REFERENCES [dbo].[CS_JobCategory] ([ID])


GO
ALTER TABLE [dbo].[CS_JobInfo] CHECK CONSTRAINT [FK_CS_JobInfo_CS_JobCategory]

