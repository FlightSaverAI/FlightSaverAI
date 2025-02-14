import { inject } from '@angular/core';
import { StatisticsService } from '@flight-saver/statistics/data-access';
import { TopOverviewChartConfig } from '@flight-saver/statistics/models';
import { map, Observable } from 'rxjs';

export const createTopOverviewChartConfig = (): Observable<TopOverviewChartConfig[]> => {
  return inject(StatisticsService)
    .getTopOverview()
    .pipe(
      map((data) => {
        return [
          {
            imageSrc: 'global/assets/images/top-airports.jpg',
            cardTitle: 'TOP AIRPORTS',
            title: {
              text: '',
            },
            tooltip: {},
            xAxis: {
              type: 'value',
              show: false,
            },
            yAxis: {
              type: 'category',
              data: Array.from(Object.keys(data.topAirports)),
              axisLabel: {
                color: '#FFFFFF',
                fontWeight: 'bold',
              },
            },
            series: [
              {
                type: 'bar',
                data: Array.from(Object.values(data.topAirports)),
                barWidth: 20,
                label: {
                  show: true,
                  position: 'right',
                  color: '#FFFFFF',
                  fontWeight: 'bold',
                },
                itemStyle: {
                  borderRadius: [5, 5, 5, 5],
                  color: {
                    type: 'linear',
                    x: 0,
                    y: 0,
                    x2: 1,
                    y2: 0,
                    colorStops: [
                      {
                        offset: 0,
                        color: '#7683af',
                      },
                      {
                        offset: 1,
                        color: '#b7c2e8',
                      },
                    ],
                  },
                },
              },
            ],
            grid: {
              left: '25%',
              right: '10%',
              bottom: '10%',
              top: '10%',
            },
          },
          {
            imageSrc: 'global/assets/images/top-airlines.jpg',
            cardTitle: 'TOP AIRLINES',
            tooltip: {},
            xAxis: {
              type: 'value',
              show: false,
            },
            yAxis: {
              type: 'category',
              data: Array.from(Object.keys(data.topAirlines)),
              axisLabel: {
                textStyle: {
                  color: '#FFFFFF',
                  fontWeight: 'bold',
                },
              },
            },
            series: [
              {
                type: 'bar',
                data: Array.from(Object.values(data.topAirlines)),
                barWidth: 20,
                barCategoryGap: '30%',
                label: {
                  show: true,
                  position: 'right',
                  color: '#FFFFFF',
                  fontWeight: 'bold',
                },
                itemStyle: {
                  borderRadius: [5, 5, 5, 5],
                  color: {
                    type: 'linear',
                    x: 0,
                    y: 0,
                    x2: 1,
                    y2: 0,
                    colorStops: [
                      {
                        offset: 0,
                        color: '#7683af',
                      },
                      {
                        offset: 1,
                        color: '#b7c2e8',
                      },
                    ],
                  },
                },
              },
            ],
            grid: {
              left: '25%',
              right: '10%',
              bottom: '10%',
              top: '10%',
            },
          },
          {
            imageSrc: 'global/assets/images/top-routes.jpg',
            cardTitle: 'TOP ROUTES',
            title: {
              text: '',
            },
            tooltip: {},
            xAxis: {
              type: 'value',
              show: false,
            },
            yAxis: {
              type: 'category',
              data: Array.from(Object.keys(data.flightRoutes)),
              axisLabel: {
                textStyle: {
                  color: '#FFFFFF',
                  fontWeight: 'bold',
                },
              },
            },
            series: [
              {
                type: 'bar',
                data: Array.from(Object.values(data.flightRoutes)),
                barWidth: 20,
                barCategoryGap: '30%',
                barGap: '0%',
                label: {
                  show: true,
                  position: 'right',
                  color: '#FFFFFF',
                  fontWeight: 'bold',
                },
                itemStyle: {
                  borderRadius: [5, 5, 5, 5],
                  color: {
                    type: 'linear',
                    x: 0,
                    y: 0,
                    x2: 1,
                    y2: 0,
                    colorStops: [
                      {
                        offset: 0,
                        color: '#7683af',
                      },
                      {
                        offset: 1,
                        color: '#b7c2e8',
                      },
                    ],
                  },
                },
              },
            ],
            grid: {
              left: '25%',
              right: '10%',
              bottom: '10%',
              top: '10%',
            },
          },
        ];
      })
    );
};
