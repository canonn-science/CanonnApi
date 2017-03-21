import {async, ComponentFixture, TestBed} from '@angular/core/testing';
import {ObeliskComponent} from './obelisk.component';

describe('ObeliskComponent', () => {
	let component: ObeliskComponent;
	let fixture: ComponentFixture<ObeliskComponent>;

	beforeEach(async(() => {
		TestBed.configureTestingModule({
			declarations: [ObeliskComponent]
		})
			.compileComponents();
	}));

	beforeEach(() => {
		fixture = TestBed.createComponent(ObeliskComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
