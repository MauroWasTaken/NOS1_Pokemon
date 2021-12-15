import fs from 'fs';

/**
 * This class is designed to write in a file.
 */
export class FileWriter {

  /**
   * Write content in a file.
   *
   * @param filePath
   * @param content
   */
  public write(filePath: string, content: string) {
    fs.writeFileSync(filePath, content);
  }
}
