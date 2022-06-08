export function timePeriodToCron(timePeriod: string): string {
  console.log("💫", timePeriod)
  const [days, time] = timePeriod.split(';');
  const [hours, mins] = time.split(':');
  const cronStr = `0 ${mins} ${hours} ? * ${days}`
  return cronStr;
}
