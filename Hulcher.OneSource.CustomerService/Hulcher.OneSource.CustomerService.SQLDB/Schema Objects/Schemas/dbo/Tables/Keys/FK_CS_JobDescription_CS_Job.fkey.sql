ALTER TABLE [dbo].[CS_JobDescription]  WITH CHECK ADD  CONSTRAINT [FK_CS_JobDescription_CS_Job] FOREIGN KEY([JobId])
REFERENCES [dbo].[CS_Job] ([ID])


GO
ALTER TABLE [dbo].[CS_JobDescription] CHECK CONSTRAINT [FK_CS_JobDescription_CS_Job]

