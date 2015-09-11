ALTER TABLE [dbo].[CS_JobDivision]  WITH CHECK ADD  CONSTRAINT [FK_CS_JobDivision_CS_Division] FOREIGN KEY([DivisionID])
REFERENCES [dbo].[CS_Division] ([ID])


GO
ALTER TABLE [dbo].[CS_JobDivision] CHECK CONSTRAINT [FK_CS_JobDivision_CS_Division]

