﻿ALTER TABLE [dbo].[CS_JobInfo]  WITH CHECK ADD  CONSTRAINT [FK_CS_JobInfo_CS_Job] FOREIGN KEY([JobID])
REFERENCES [dbo].[CS_Job] ([ID])


GO
ALTER TABLE [dbo].[CS_JobInfo] CHECK CONSTRAINT [FK_CS_JobInfo_CS_Job]

