/* Content Configurations Model */
export class ContentConfigs {
  fieldName: string; 
  fieldDisplayName: string;
  formula: string;
  group: string;
  contentType: string;
  constructor (contentConfigs?) {
    this.fieldName = contentConfigs.fieldName || '';
    this.fieldDisplayName = contentConfigs.fieldDisplayName || '';
    this.formula = contentConfigs.formula || '';
    this.group = contentConfigs.group || '';
    this.contentType = contentConfigs.contentType || '';
  }
}
