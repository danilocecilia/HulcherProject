﻿ALTER TABLE [dbo].[CS_CallLog]  WITH CHECK ADD  CONSTRAINT [FK_CS_CallLog_CS_CallType] FOREIGN KEY([CallTypeID])
REFERENCES [dbo].[CS_CallType] ([ID])


GO
ALTER TABLE [dbo].[CS_CallLog] CHECK CONSTRAINT [FK_CS_CallLog_CS_CallType]

