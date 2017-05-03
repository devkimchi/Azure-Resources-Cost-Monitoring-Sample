@echo off

REM Check entire history over total spend limit with threshold value
CostControlWarning.exe -e -h 0.9

REM Check entire history over total spend limit
CostControlWarning.exe -e

REM Check daily usage over daily spend limit
CostControlWarning.exe -r 2 -d 2
