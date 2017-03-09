import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RelictsComponent } from './relicts.component';

describe('RelictsComponent', () => {
  let component: RelictsComponent;
  let fixture: ComponentFixture<RelictsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RelictsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RelictsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
