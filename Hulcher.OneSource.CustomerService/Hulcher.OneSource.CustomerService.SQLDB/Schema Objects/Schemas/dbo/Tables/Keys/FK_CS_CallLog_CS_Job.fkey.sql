﻿ALTER TABLE [dbo].[CS_CallLog]  WITH CHECK ADD  CONSTRAINT [FK_CS_CallLog_CS_Job] FOREIGN KEY([JobID])
REFERENCES [dbo].[CS_Job] ([ID])


GO
ALTER TABLE [dbo].[CS_CallLog] CHECK CONSTRAINT [FK_CS_CallLog_CS_Job]

