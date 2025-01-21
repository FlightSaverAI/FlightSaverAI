import { inject } from '@angular/core';
import { StatisticsService } from '@flight-saver/statistics/data-access';
import { map } from 'rxjs';

export const createActivityOverviewChartConfig = () => {
  return inject(StatisticsService)
    .getActivityOverview()
    .pipe(
      map((data) => {
        return [
          {
            cardTitle: 'FLIGHT PER MONTH',
            imageSrc: 'global/assets/images/activity-per-month.jpeg',
            tooltip: {
              trigger: 'axis',
              axisPointer: {
                type: 'line',
              },
            },
            xAxis: {
              type: 'category',
              data: [
                'JAN',
                'FEB',
                'MAR',
                'APR',
                'MAY',
                'JUN',
                'JUL',
                'AUG',
                'SEP',
                'OCT',
                'NOV',
                'DEC',
              ],
              axisLabel: {
                color: '#FFFFFF',
                interval: 0,
              },
              axisLine: {
                lineStyle: {
                  color: '#FFFFFF',
                },
              },
            },
            yAxis: {
              type: 'value',
              name: 'Flights',
              axisLabel: {
                color: '#FFFFFF',
              },
              axisLine: {
                lineStyle: {
                  color: '#FFFFFF',
                },
              },
              splitLine: {
                lineStyle: {
                  color: 'transparent',
                },
              },
            },
            series: [
              {
                name: 'Flights',
                type: 'line',
                smooth: true,
                data: Array.from(Object.values(data.flightsPerMonth)),
                itemStyle: {
                  color: 'white',
                },
                lineStyle: {
                  width: 2,
                },
                symbol: 'circle',
                symbolSize: 8,
              },
            ],
          },
          {
            cardTitle: 'FLIGHT PER WEEK',
            imageSrc: 'global/assets/images/activity-per-week.jpeg',
            tooltip: {
              trigger: 'axis',
              axisPointer: {
                type: 'line',
              },
            },
            xAxis: {
              type: 'category',
              data: ['MON', 'TUE', 'WED', 'THU', 'FRI', 'SAT', 'SUN'],
              axisLabel: {
                color: '#FFFFFF',
                interval: 0,
              },
              axisLine: {
                lineStyle: {
                  color: '#FFFFFF',
                },
              },
              boundaryGap: false,
            },
            yAxis: {
              type: 'value',
              name: 'Flights',
              axisLabel: {
                color: '#FFFFFF',
              },
              axisLine: {
                lineStyle: {
                  color: '#FFFFFF',
                },
              },
              splitLine: {
                lineStyle: {
                  color: 'transparent',
                },
              },
            },
            series: [
              {
                name: 'Flights',
                type: 'line',
                smooth: true,
                data: Array.from(Object.values(data.flightsPerWeek)),
                itemStyle: {
                  color: 'white',
                },
                lineStyle: {
                  width: 2,
                },
                symbol: 'circle',
                symbolSize: 8,
              },
            ],
          },
        ];
      })
    );
};
