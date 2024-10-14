import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StreamlinedSoaringSectionComponent } from './streamlined-soaring-section.component';

describe('StreamlinedSoaringSectionComponent', () => {
  let component: StreamlinedSoaringSectionComponent;
  let fixture: ComponentFixture<StreamlinedSoaringSectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StreamlinedSoaringSectionComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(StreamlinedSoaringSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
