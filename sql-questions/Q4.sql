-- question 1
select ex.[ExerName], std.[ClassId], count(1) [studnet-count]
from Students std
         join Examiner ex on ex.[ClassId] = std.[ClassId]
group by ex.[ExerName], std.[ClassId]

-- question 2
select top 1  s.[StdName]
from Exam e
         join dbo.Students S on e.StdID = S.StdID
where e.[ExamType] = 'Final'
group by e.[StdID], s.[StdName]
order by sum(e.[Score]) desc