﻿ALTER TABLE [dbo].[CS_JobInfo]  WITH CHECK ADD  CONSTRAINT [FK_CS_JobInfo_CS_JobType] FOREIGN KEY([JobTypeID])
REFERENCES [dbo].[CS_JobType] ([ID])


GO
ALTER TABLE [dbo].[CS_JobInfo] CHECK CONSTRAINT [FK_CS_JobInfo_CS_JobType]
