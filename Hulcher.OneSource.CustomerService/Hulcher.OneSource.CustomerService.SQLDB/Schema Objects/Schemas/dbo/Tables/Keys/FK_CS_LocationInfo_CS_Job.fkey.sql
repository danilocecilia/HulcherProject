ALTER TABLE [dbo].[CS_LocationInfo]  WITH CHECK ADD  CONSTRAINT [FK_CS_LocationInfo_CS_Job] FOREIGN KEY([JobID])
REFERENCES [dbo].[CS_Job] ([ID])


GO
ALTER TABLE [dbo].[CS_LocationInfo] CHECK CONSTRAINT [FK_CS_LocationInfo_CS_Job]

