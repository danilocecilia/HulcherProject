ALTER TABLE [dbo].[CS_JobDivision]  WITH CHECK ADD  CONSTRAINT [FK_CS_JobDivision_CS_Job] FOREIGN KEY([JobID])
REFERENCES [dbo].[CS_Job] ([ID])


GO
ALTER TABLE [dbo].[CS_JobDivision] CHECK CONSTRAINT [FK_CS_JobDivision_CS_Job]

