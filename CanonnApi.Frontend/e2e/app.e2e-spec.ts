import { RuinsAppPage } from './app.po';

describe('ruins-app App', () => {
  let page: RuinsAppPage;

  beforeEach(() => {
    page = new RuinsAppPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
