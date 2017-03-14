import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {RuinLayoutComponent} from './ruinLayout.component';

describe('RuinLayoutComponent', () => {
	let component: RuinLayoutComponent;
	let fixture: ComponentFixture<RuinLayoutComponent>;

	beforeEach(async(() => {
		TestBed.configureTestingModule({
			declarations: [RuinLayoutComponent]
		})
			.compileComponents();
	}));

	beforeEach(() => {
		fixture = TestBed.createComponent(RuinLayoutComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
