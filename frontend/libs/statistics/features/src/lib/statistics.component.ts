import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FlightPassengerStatisticsComponent } from '@flight-saver/statistics/ui';
import { TopStatisticsComponent } from '@flight-saver/statistics/ui';
import { EChartsOption } from 'echarts';

@Component({
  standalone: true,
  imports: [CommonModule, FlightPassengerStatisticsComponent, TopStatisticsComponent],
  template: `<statistics-flight-passenger-statistics
    [flightPassangerStatistics]="flightPassangerStatistics"
  ></statistics-flight-passenger-statistics> `,
  styleUrl: './statistics.component.scss',
})
export class StatisticsComponent {
  flightPassangerStatistics: EChartsOption[] = [
    {
      title: {
        text: 'CLASS',
        left: 'center',
        textStyle: {
          color: '#FFFFFF',
        },
        top: '5%',
      },
      tooltip: {
        trigger: 'item',
      },
      legend: {
        orient: 'vertical',
        bottom: '5%',
        left: 'center',
        icon: 'circle',
        textStyle: {
          color: '#FFFFFF',
        },
        formatter: function (name: string) {
          const data: any = {
            Economy: 7,
            'Economy+': 4,
            Business: 1,
            First: 0,
            Private: 0,
          };
          return `${data[name]}  ${name}`;
        },
      },
      color: ['#B7C2E8', '#4B5169', '#FFFFFF', '#C4C4C4', '#7682AF'],
      series: [
        {
          name: 'Access From',
          type: 'pie',
          radius: '60%',
          center: ['50%', '40%'],
          data: [
            { value: 7, name: 'Economy' },
            { value: 4, name: 'Economy+' },
            { value: 1, name: 'Business' },
            { value: 0, name: 'First' },
            { value: 0, name: 'Private' },
          ],
          label: {
            show: false,
          },
          emphasis: {
            itemStyle: {
              shadowBlur: 10,
              shadowOffsetX: 0,
              shadowColor: 'rgba(0, 0, 0, 0.5)',
            },
          },
        },
      ],
    },
    {
      title: {
        text: 'SEAT',
        left: 'center',
        textStyle: {
          color: '#FFFFFF',
        },
        top: '5%',
      },
      tooltip: {
        trigger: 'item',
      },
      legend: {
        orient: 'vertical',
        bottom: '10%',
        left: 'center',
        icon: 'circle',
        textStyle: {
          color: '#FFFFFF',
        },
        formatter: function (name: string) {
          const data: any = {
            aisle: 3,
            middle: 3,
            window: 3,
          };
          return `${data[name]}  ${name}`;
        },
      },
      color: ['#B7C2E8', '#4B5169', '#FFFFFF', '#C4C4C4', '#7682AF'],
      series: [
        {
          name: 'Access From',
          type: 'pie',
          radius: '60%',
          center: ['50%', '40%'],
          data: [
            { value: 3, name: 'aisle' },
            { value: 3, name: 'middle' },
            { value: 3, name: 'window' },
          ],
          label: {
            show: false,
          },
          emphasis: {
            itemStyle: {
              shadowBlur: 10,
              shadowOffsetX: 0,
              shadowColor: 'rgba(0, 0, 0, 0.5)',
            },
          },
        },
      ],
    },
    {
      title: {
        text: 'REASON',
        left: 'center',
        textStyle: {
          color: '#FFFFFF',
        },
        top: '5%',
      },
      tooltip: {
        trigger: 'item',
      },
      legend: {
        orient: 'vertical',
        bottom: '5%',
        left: 'center',
        icon: 'circle',
        textStyle: {
          color: '#FFFFFF',
        },
        formatter: function (name: string) {
          const data: any = {
            leisure: 6,
            business: 3,
            crew: 1,
            other: 1,
          };
          return `${data[name]}  ${name}`;
        },
      },
      color: ['#B7C2E8', '#4B5169', '#FFFFFF', '#C4C4C4', '#7682AF'],
      series: [
        {
          name: 'Access From',
          type: 'pie',
          radius: '60%',
          center: ['50%', '40%'],
          data: [
            { value: 6, name: 'leisure' },
            { value: 3, name: 'business' },
            { value: 1, name: 'crew' },
            { value: 1, name: 'other' },
          ],
          label: {
            show: false,
          },
          emphasis: {
            itemStyle: {
              shadowBlur: 10,
              shadowOffsetX: 0,
              shadowColor: 'rgba(0, 0, 0, 0.5)',
            },
          },
        },
      ],
    },
    {
      title: {
        text: 'CONTINENTS',
        left: 'center',
        textStyle: {
          color: '#FFFFFF',
        },
        top: '5%',
      },
      tooltip: {
        trigger: 'item',
      },
      legend: {
        orient: 'vertical',
        bottom: '5%',
        left: 'center',
        icon: 'circle',
        textStyle: {
          color: '#FFFFFF',
        },
        formatter: function (name: string) {
          const data: any = {
            Oceania: 0,
            'South America': 0,
            'North America': 0,
            Europe: 12,
            Asia: 0,
            Africa: 0,
          };
          return `${data[name]}  ${name}`;
        },
      },
      color: ['#B7C2E8', '#4B5169', '#FFFFFF', '#C4C4C4', '#7682AF', '#9747FF'],
      series: [
        {
          name: 'Access From',
          type: 'pie',
          radius: '60%',
          center: ['50%', '40%'],
          data: [
            { value: 0, name: 'Oceania' },
            { value: 0, name: 'South America' },
            { value: 0, name: 'North America' },
            { value: 12, name: 'Europe' },
            { value: 0, name: 'Asia' },
            { value: 0, name: 'Africa' },
          ],
          label: {
            show: false,
          },
          emphasis: {
            itemStyle: {
              shadowBlur: 10,
              shadowOffsetX: 0,
              shadowColor: 'rgba(0, 0, 0, 0.5)',
            },
          },
        },
      ],
    },
  ];
}
