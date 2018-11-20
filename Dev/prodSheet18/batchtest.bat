@ECHO OFF
ECHO %~n0 was called with the following arguments:
SET args=%*
IF NOT DEFINED args GOTO END
ECHO %*
:END
PAUSE