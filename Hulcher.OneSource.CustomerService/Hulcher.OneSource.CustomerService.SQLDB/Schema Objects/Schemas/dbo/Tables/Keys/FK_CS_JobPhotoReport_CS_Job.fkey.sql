ALTER TABLE [dbo].[CS_JobPhotoReport]  WITH CHECK ADD  CONSTRAINT [FK_CS_JobPhotoReport_CS_Job] FOREIGN KEY([JobID])
REFERENCES [dbo].[CS_Job] ([ID])


GO
ALTER TABLE [dbo].[CS_JobPhotoReport] CHECK CONSTRAINT [FK_CS_JobPhotoReport_CS_Job]

