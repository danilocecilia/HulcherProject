﻿ALTER TABLE [dbo].[CS_JobPermit]  WITH CHECK ADD  CONSTRAINT [FK_CS_JobPermit_CS_Job] FOREIGN KEY([JobID])
REFERENCES [dbo].[CS_Job] ([ID])


GO
ALTER TABLE [dbo].[CS_JobPermit] CHECK CONSTRAINT [FK_CS_JobPermit_CS_Job]

