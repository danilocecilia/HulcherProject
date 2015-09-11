use OneSource
go

INSERT INTO CS_Settings Values ('Last First Alert Number', 3, 1);
INSERT INTO CS_Settings Values ('First Alert Receipts List', 2, 'cburton@dev;ksantos@dev;ccarvalho@dev');

INSERT INTO CS_FirstAlertType Values ('Injury', 1, 'System', GETDATE(), 1, 'System', GETDATE(), 1);
INSERT INTO CS_FirstAlertType Values ('Illness', 1, 'System', GETDATE(), 1, 'System', GETDATE(), 1);
INSERT INTO CS_FirstAlertType Values ('Property Damage', 1, 'System', GETDATE(), 1, 'System', GETDATE(), 1);
INSERT INTO CS_FirstAlertType Values ('Vehicle Involved', 1, 'System', GETDATE(), 1, 'System', GETDATE(), 1);
INSERT INTO CS_FirstAlertType Values ('Theft', 1, 'System', GETDATE(), 1, 'System', GETDATE(), 1);
INSERT INTO CS_FirstAlertType Values ('Fire', 1, 'System', GETDATE(), 1, 'System', GETDATE(), 1);
INSERT INTO CS_FirstAlertType Values ('Other', 1, 'System', GETDATE(), 1, 'System', GETDATE(), 1);